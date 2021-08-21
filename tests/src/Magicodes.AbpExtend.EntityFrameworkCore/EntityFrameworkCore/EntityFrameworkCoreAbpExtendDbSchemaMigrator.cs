using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Magicodes.AbpExtend.Data;
using Volo.Abp.DependencyInjection;

namespace Magicodes.AbpExtend.EntityFrameworkCore
{
    public class EntityFrameworkCoreAbpExtendDbSchemaMigrator
        : IAbpExtendDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreAbpExtendDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the AbpExtendDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<AbpExtendDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
