using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FeatureRequestProject.FeatureRequestComments
{
    public class FeatureRequestComment : CreationAuditedEntity<Guid>
    {
        public Guid FeatureRequestId { get; set; }
        public string Content { get; set; }

        protected FeatureRequestComment() { }

        public FeatureRequestComment(Guid id,  Guid featureRequestId, string content) : base(id)
        {
            FeatureRequestId = featureRequestId;
            Content = content;
        }
    }
}
