using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace FeatureRequestProject.Data;

/* This is used if database provider does't define
 * IFeatureRequestProjectDbSchemaMigrator implementation.
 */
public class NullFeatureRequestProjectDbSchemaMigrator : IFeatureRequestProjectDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
