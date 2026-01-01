using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeatureRequestProject.FeatureRequests
{
    public class CreateUpdateFeatureRequestDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Required]
        public Category CategoryId { get; set; }
    }
}
