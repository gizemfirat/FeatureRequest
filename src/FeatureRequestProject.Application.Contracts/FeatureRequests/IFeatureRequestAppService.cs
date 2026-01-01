using FeatureRequestProject.FeatureRequestVotes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FeatureRequestProject.FeatureRequests
{
    public interface IFeatureRequestAppService : ICrudAppService<
            FeatureRequestDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateFeatureRequestDto>
    {
        Task VoteAsync(Guid id, VoteType type);
        Task CreateCommentAsync(Guid id, string content);
    }
}
