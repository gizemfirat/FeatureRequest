using FeatureRequestProject.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace FeatureRequestProject.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FeatureRequestProjectEntityFrameworkCoreModule),
    typeof(FeatureRequestProjectApplicationContractsModule)
    )]
public class FeatureRequestProjectDbMigratorModule : AbpModule
{
}
