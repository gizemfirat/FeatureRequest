using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FeatureRequestProject.FeatureRequestComments
{
    public class FeatureRequestCommentDto : AuditedEntityDto<Guid>
    {
        public Guid FeatureRequestId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
    }
}
