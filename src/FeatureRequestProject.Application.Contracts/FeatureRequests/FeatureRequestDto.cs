using FeatureRequestProject.FeatureRequestComments;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FeatureRequestProject.FeatureRequests
{
    public class FeatureRequestDto : AuditedEntityDto<Guid>
    {
        public string CreatorUserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int VoteCount { get; set; }
        public Category CategoryId { get; set; }
        public List<FeatureRequestCommentDto> Comments { get; set; } = new();
        public int CurrentUserVote { get; set; }
    }
}
