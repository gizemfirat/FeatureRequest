using Microsoft.Extensions.Localization;
using FeatureRequestProject.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace FeatureRequestProject.Web;

[Dependency(ReplaceServices = true)]
public class FeatureRequestProjectBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<FeatureRequestProjectResource> _localizer;

    public FeatureRequestProjectBrandingProvider(IStringLocalizer<FeatureRequestProjectResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
