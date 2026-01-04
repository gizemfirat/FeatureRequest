using FeatureRequestProject.FeatureRequestComments;
using FeatureRequestProject.FeatureRequestVotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace FeatureRequestProject.FeatureRequests
{
    public class FeatureRequest : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int VoteCount { get; set; }
        public Category CategoryId { get; set; }
        public virtual ICollection<FeatureRequestVote> Votes { get; private set; }
        public virtual ICollection<FeatureRequestComment> Comments { get; set; } = new Collection<FeatureRequestComment>();

        protected FeatureRequest() {
            Comments = new Collection<FeatureRequestComment>();
        }

        public FeatureRequest(
            Guid id,
            string title,
            string description,
            Category categoryId
        ) : base(id)
        {
            Title = title;
            Description = description;
            Status = Status.Pending;
            CategoryId = categoryId;
            Votes = new Collection<FeatureRequestVote>();
            Comments = new Collection<FeatureRequestComment>();
        }

        public void SetVote(Guid userId, VoteType type, IGuidGenerator guidGenerator) 
        {
            Votes ??= new Collection<FeatureRequestVote>();

            var existingVote = Votes.FirstOrDefault(v => v.CreatorId == userId);

            if (existingVote != null)
            {
                if (existingVote.Value == type)
                {
                    Votes.Remove(existingVote);
                }
                else
                {
                    existingVote.Value = type;
                }
            }
            else 
            {
                Votes.Add(new FeatureRequestVote(guidGenerator.Create(), Id, type));
            }

            VoteCount = Votes.Count;
        }

        public void AddComment(string content, IGuidGenerator guidGenerator) 
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new BusinessException("ContentCannotBeEmpty");
            }

            Comments ??= new Collection<FeatureRequestComment>();
            Comments.Add(new FeatureRequestComment(guidGenerator.Create(), Id, content));
        }
    }
}
