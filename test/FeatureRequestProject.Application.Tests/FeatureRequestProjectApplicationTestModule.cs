using Volo.Abp.Modularity;

namespace FeatureRequestProject;

[DependsOn(
    typeof(FeatureRequestProjectApplicationModule),
    typeof(FeatureRequestProjectDomainTestModule)
)]
public class FeatureRequestProjectApplicationTestModule : AbpModule
{

}
