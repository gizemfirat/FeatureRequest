using System;
using System.Collections.Generic;
using System.Text;
using FeatureRequestProject.Localization;
using Volo.Abp.Application.Services;

namespace FeatureRequestProject;

/* Inherit your application services from this class.
 */
public abstract class FeatureRequestProjectAppService : ApplicationService
{
    protected FeatureRequestProjectAppService()
    {
        LocalizationResource = typeof(FeatureRequestProjectResource);
    }
}
