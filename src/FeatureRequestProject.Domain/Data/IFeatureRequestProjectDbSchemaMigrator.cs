using System.Threading.Tasks;

namespace FeatureRequestProject.Data;

public interface IFeatureRequestProjectDbSchemaMigrator
{
    Task MigrateAsync();
}
