using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Magicodes.AbpExtend.Data
{
    /* This is used if database provider does't define
     * IAbpExtendDbSchemaMigrator implementation.
     */
    public class NullAbpExtendDbSchemaMigrator : IAbpExtendDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}