using FeatureRequestProject.FeatureRequestComments;
using FeatureRequestProject.FeatureRequestVotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FeatureRequestProject.FeatureRequests
{
    public class FeatureRequest : AuditedAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int VoteCount { get; set; }
        public Category CategoryId { get; set; }
        public Collection<FeatureRequestVote> Votes { get; set; }
        public Collection<FeatureRequestComment> Comments { get; set; }
    }
}
