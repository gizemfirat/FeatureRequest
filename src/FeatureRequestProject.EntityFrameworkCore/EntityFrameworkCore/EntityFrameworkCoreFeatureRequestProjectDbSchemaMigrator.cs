using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FeatureRequestProject.Data;
using Volo.Abp.DependencyInjection;

namespace FeatureRequestProject.EntityFrameworkCore;

public class EntityFrameworkCoreFeatureRequestProjectDbSchemaMigrator
    : IFeatureRequestProjectDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreFeatureRequestProjectDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the FeatureRequestProjectDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FeatureRequestProjectDbContext>()
            .Database
            .MigrateAsync();
    }
}
