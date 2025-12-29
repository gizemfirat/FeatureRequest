using Volo.Abp.Modularity;

namespace FeatureRequestProject;

public abstract class FeatureRequestProjectApplicationTestBase<TStartupModule> : FeatureRequestProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
