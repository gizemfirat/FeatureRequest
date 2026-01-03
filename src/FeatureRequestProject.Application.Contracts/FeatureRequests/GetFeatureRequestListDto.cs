using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FeatureRequestProject.FeatureRequests
{
    public class GetFeatureRequestListDto : PagedAndSortedResultRequestDto
    {
        public Category? Category { get; set; }
        public string? Filter { get; set; }
        public bool? IsMyRequests { get; set; }
    }
}
