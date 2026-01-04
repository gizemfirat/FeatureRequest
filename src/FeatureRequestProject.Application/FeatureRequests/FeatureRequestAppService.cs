using FeatureRequestProject.FeatureRequestVotes;
using FeatureRequestProject.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace FeatureRequestProject.FeatureRequests
{
    public class FeatureRequestAppService : CrudAppService<
            FeatureRequest,
            FeatureRequestDto,
            Guid,
            GetFeatureRequestListDto,
            CreateUpdateFeatureRequestDto>,
        IFeatureRequestAppService
    {
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        public FeatureRequestAppService(
            IRepository<FeatureRequest, Guid> repository,
            IRepository<IdentityUser, Guid> userRepository)
            : base(repository)
        {
            _userRepository = userRepository;

            UpdatePolicyName = FeatureRequestProjectPermissions.FeatureRequests.Edit;
            DeletePolicyName = FeatureRequestProjectPermissions.FeatureRequests.Delete;
        }

        [Authorize]
        public override async Task<FeatureRequestDto> CreateAsync(CreateUpdateFeatureRequestDto input)
        {
            var featureRequest = ObjectMapper.Map<CreateUpdateFeatureRequestDto, FeatureRequest>(input);

            featureRequest.Status = Status.Pending;
            featureRequest.VoteCount = 0;

            await Repository.InsertAsync(featureRequest);

            return ObjectMapper.Map<FeatureRequest, FeatureRequestDto>(featureRequest);
        }

        [Authorize]
        public async Task VoteAsync(Guid id, VoteType type)
        {
            var queryable = await Repository.WithDetailsAsync(x => x.Votes);
            var featureRequest = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);

            if (featureRequest == null) { throw new EntityNotFoundException(); }

            featureRequest.SetVote(CurrentUser.GetId(), type, GuidGenerator);

            await Repository.UpdateAsync(featureRequest);
        }

        [Authorize]
        public async Task CreateCommentAsync(Guid id, string content)
        {
            var queryable = await Repository.WithDetailsAsync(x => x.Comments);
            var featureRequest = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);

            if (featureRequest == null)
            {
                throw new EntityNotFoundException();
            }

            featureRequest.AddComment(content, GuidGenerator);
            await Repository.UpdateAsync(featureRequest);
        }

        public override async Task<PagedResultDto<FeatureRequestDto>> GetListAsync(GetFeatureRequestListDto input)
        {
            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var dtos = ObjectMapper.Map<List<FeatureRequest>, List<FeatureRequestDto>>(entities);

            var creatorIds = dtos
                .Where(x => x.CreatorId.HasValue)
                .Select(x => x.CreatorId.Value)
                .Distinct()
                .ToList();

            if (creatorIds.Any())
            {
                var users = await _userRepository.GetListAsync(u => creatorIds.Contains(u.Id));
                var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

                foreach (var dto in dtos)
                {
                    if (dto.CreatorId.HasValue && userDictionary.ContainsKey(dto.CreatorId.Value))
                    {
                        dto.CreatorUserName = userDictionary[dto.CreatorId.Value];
                    }
                    else
                    {
                        dto.CreatorUserName = "Unknown";
                    }
                }
            }

            return new PagedResultDto<FeatureRequestDto>(totalCount, dtos);
        }

        public override async Task<FeatureRequestDto> GetAsync(Guid id)
        {
            var queryable = await Repository.WithDetailsAsync(x => x.Comments, x => x.Votes);
            var entity = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(FeatureRequest), id);
            }

            var dto = ObjectMapper.Map<FeatureRequest, FeatureRequestDto>(entity);

            if (entity.Votes != null)
            {
                dto.UpVoteCount = entity.Votes.Count(v => v.Value == VoteType.Upvote);
                dto.DownVoteCount = entity.Votes.Count(v => v.Value == VoteType.Downvote);
            }

            if (CurrentUser.Id.HasValue && entity.Votes != null)
            {
                var myVote = entity.Votes.FirstOrDefault(v => v.CreatorId == CurrentUser.Id);
                if (myVote != null)
                {
                    dto.CurrentUserVote = (int)myVote.Value;
                }
            }

            if (dto.CreatorId.HasValue)
            {
                var creator = await _userRepository.FindAsync(dto.CreatorId.Value);
                dto.CreatorUserName = creator?.UserName ?? "Anonim";
            }
            if (dto.Comments != null && dto.Comments.Any())
            {
                var creatorIds = dto.Comments
                                .Where(c => c.CreatorId.HasValue)
                                .Select(c => c.CreatorId.Value)
                                .Distinct()
                                .ToList();

                var users = await _userRepository.GetListAsync(u => creatorIds.Contains(u.Id));

                var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

                foreach (var commentDto in dto.Comments)
                {
                    if (commentDto.CreatorId.HasValue && userDictionary.ContainsKey(commentDto.CreatorId.Value))
                    {
                        commentDto.UserName = userDictionary[commentDto.CreatorId.Value];
                    }
                    else
                    {
                        commentDto.UserName = "Anonim";
                    }
                }
            }

            return dto;
        }

        protected override async Task<IQueryable<FeatureRequest>> CreateFilteredQueryAsync(GetFeatureRequestListDto input)
        {
            var query = await base.CreateFilteredQueryAsync(input);

            query = query
                .WhereIf(input.Category.HasValue, x => x.CategoryId == input.Category)
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Title.Contains(input.Filter));

            if (input.IsMyRequests == true && CurrentUser.Id.HasValue)
            {
                query = query.Where(x => x.CreatorId == CurrentUser.Id);
            }

            return query;
        }

        public override async Task<FeatureRequestDto> UpdateAsync(Guid id, CreateUpdateFeatureRequestDto input)
        {
            var entity = await Repository.GetAsync(id);
            await AuthorizationService.CheckAsync(entity, new OperationAuthorizationRequirement { Name = "Update" });

            ObjectMapper.Map(input, entity);
            await Repository.UpdateAsync(entity);
            return ObjectMapper.Map<FeatureRequest, FeatureRequestDto>(entity);
        }

        public override async Task DeleteAsync(Guid id)
        {
            var entity = await Repository.GetAsync(id);
            await AuthorizationService.CheckAsync(entity, new OperationAuthorizationRequirement { Name = "Delete" });

            await Repository.DeleteAsync(entity);
        }
    }
}
