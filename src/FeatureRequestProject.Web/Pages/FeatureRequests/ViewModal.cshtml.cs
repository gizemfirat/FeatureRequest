using FeatureRequestProject.FeatureRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FeatureRequestProject.Web.Pages.FeatureRequests
{
    public class ViewModalModel : FeatureRequestProjectPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public string NewCommentContent { get; set; }

        public FeatureRequestDto FeatureRequest { get; set; }

        private readonly IFeatureRequestAppService _featureRequestAppService;

        public ViewModalModel(IFeatureRequestAppService featureRequestAppService)
        {
            _featureRequestAppService = featureRequestAppService;
        }
        public async Task OnGetAsync()
        {
            FeatureRequest = await _featureRequestAppService.GetAsync(Id);
        }
    }
}
