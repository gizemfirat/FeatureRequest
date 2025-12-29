using Volo.Abp.Modularity;

namespace FeatureRequestProject;

/* Inherit from this class for your domain layer tests. */
public abstract class FeatureRequestProjectDomainTestBase<TStartupModule> : FeatureRequestProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
