using FeatureRequestProject.FeatureRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FeatureRequestProject.Web.Pages.FeatureRequests
{
    public class EditModalModel : FeatureRequestProjectPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateFeatureRequestDto FeatureRequest { get; set; }

        private readonly IFeatureRequestAppService _featureRequestAppService;

        public EditModalModel(IFeatureRequestAppService featureRequestAppService)
        {
            _featureRequestAppService = featureRequestAppService;
        }

        public async Task OnGetAsync()
        {
            var featureRequest = await  _featureRequestAppService.GetAsync(Id);
            FeatureRequest = ObjectMapper.Map<FeatureRequestDto, CreateUpdateFeatureRequestDto>(featureRequest);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult<IActionResult>(Page());
            }
            
            await _featureRequestAppService.UpdateAsync(Id, FeatureRequest);
            return NoContent();
        }
    }
}
