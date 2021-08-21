using Magicodes.AbpExtend.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Magicodes.AbpExtend.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpExtendEntityFrameworkCoreModule),
        typeof(AbpExtendApplicationContractsModule)
        )]
    public class AbpExtendDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
