using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FeatureRequestProject.FeatureRequestVotes
{
    public class FeatureRequestVote : CreationAuditedAggregateRoot<Guid>
    {
        public VoteType Value { get; set; }
        public Guid FeatureRequestId { get; set; }

        public FeatureRequestVote(Guid id, Guid featureRequestId, VoteType value) : base(id)
        {
            FeatureRequestId = featureRequestId;
            Value = value;
        }
    }
}
