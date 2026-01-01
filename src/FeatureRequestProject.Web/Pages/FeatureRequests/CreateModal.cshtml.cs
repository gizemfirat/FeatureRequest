using FeatureRequestProject.FeatureRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FeatureRequestProject.Web.Pages.FeatureRequests
{
    public class CreateModalModel : FeatureRequestProjectPageModel
    {
        [BindProperty]
        public CreateUpdateFeatureRequestDto FeatureRequest { get; set; }

        private readonly IFeatureRequestAppService _featureRequestAppService;

        public CreateModalModel(IFeatureRequestAppService featureRequestAppService)
        {
            _featureRequestAppService = featureRequestAppService;
        }
        public void OnGet()
        {
            FeatureRequest = new CreateUpdateFeatureRequestDto();
        }

        [Authorize]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _featureRequestAppService.CreateAsync(FeatureRequest);
            return NoContent();
        }
    }
}
