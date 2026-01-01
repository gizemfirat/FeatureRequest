using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
        public FeatureRequestAppService(IRepository<FeatureRequest, Guid> repository)
            : base(repository) 
        {
            
        }
    }
}
