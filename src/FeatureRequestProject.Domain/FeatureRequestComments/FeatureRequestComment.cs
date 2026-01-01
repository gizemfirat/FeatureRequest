using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FeatureRequestProject.FeatureRequestComments
{
    public class FeatureRequestComment : AuditedAggregateRoot<Guid>
    {
        public Guid FeatureRequestId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }

        protected FeatureRequestComment() { }

        public FeatureRequestComment(Guid id,  Guid featureRequestId, Guid userId, string content) : base(id)
        {
            FeatureRequestId = featureRequestId;
            UserId = userId;
            Content = content;
        }
    }
}
