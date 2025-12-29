using FeatureRequestProject.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FeatureRequestProject.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class FeatureRequestProjectPageModel : AbpPageModel
{
    protected FeatureRequestProjectPageModel()
    {
        LocalizationResourceType = typeof(FeatureRequestProjectResource);
    }
}
