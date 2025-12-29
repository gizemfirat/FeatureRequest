using Volo.Abp.Modularity;

namespace FeatureRequestProject;

[DependsOn(
    typeof(FeatureRequestProjectDomainModule),
    typeof(FeatureRequestProjectTestBaseModule)
)]
public class FeatureRequestProjectDomainTestModule : AbpModule
{

}
