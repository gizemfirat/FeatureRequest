using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FeatureRequestProject.FeatureRequestVotes
{
    public class FeatureRequestVote : AuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public VoteType VoteType { get; set; }
        public Guid FeatureRequestId { get; set; }
        public Guid UserId { get; set; }
    }
}
