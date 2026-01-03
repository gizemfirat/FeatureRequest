using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeatureRequestProject.Web.Pages.FeatureRequests
{
    [Authorize]
    public class MyFeatureRequestsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
