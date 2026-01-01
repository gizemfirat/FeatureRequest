using FeatureRequestProject.FeatureRequestVotes;
using FeatureRequestProject.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace FeatureRequestProject.FeatureRequests
{
    public class FeatureRequestAppService : CrudAppService<
            FeatureRequest,
            FeatureRequestDto,
            Guid,
            PagedAndSortedResultRequestDto,
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

            featureRequest.AddComment(CurrentUser.GetId(), content, GuidGenerator);
            await Repository.UpdateAsync(featureRequest);
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

            if (dto.Comments != null && dto.Comments.Any())
            {
                var userIds = dto.Comments.Select(c => c.UserId).Distinct().ToList();
                var users = await _userRepository.GetListAsync(u => userIds.Contains(u.Id));

                var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

                foreach (var commentDto in dto.Comments)
                {
                    if (userDictionary.ContainsKey(commentDto.UserId))
                    {
                        commentDto.UserName = userDictionary[commentDto.UserId];
                    }
                    else
                    {
                        commentDto.UserName = "Anonim";
                    }
                }
            }

            return dto;
        }

        protected override async Task<IQueryable<FeatureRequest>> CreateFilteredQueryAsync(PagedAndSortedResultRequestDto input)
        {
            return (await base.CreateFilteredQueryAsync(input))
                .Include(x => x.Comments)
                .Include(x => x.Votes);
        }
    }
}
