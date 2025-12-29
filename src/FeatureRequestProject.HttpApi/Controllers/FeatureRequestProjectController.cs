using FeatureRequestProject.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FeatureRequestProject.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class FeatureRequestProjectController : AbpControllerBase
{
    protected FeatureRequestProjectController()
    {
        LocalizationResource = typeof(FeatureRequestProjectResource);
    }
}
