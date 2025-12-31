using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FeatureRequestProject.FeatureRequestComments
{
    public class FeatureRequestComment : AuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public Guid FeatureRequestId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
