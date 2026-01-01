using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
