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
        public Guid UserId { get; set; }

        public FeatureRequestVote(Guid id, Guid featureRequestId,  Guid userId, VoteType value) : base(id)
        {
            FeatureRequestId = featureRequestId;
            UserId = userId;
            Value = value;
        }
    }
}
