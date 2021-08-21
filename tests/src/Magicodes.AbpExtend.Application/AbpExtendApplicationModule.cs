using Magicodes.AbpExtend.Localization;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Magicodes.AbpExtend
{
    [DependsOn(
        typeof(AbpExtendDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpExtendApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule)
        )]
    public class AbpExtendApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpExtendApplicationModule>();
            });

            //Configure<AbpLocalizationOptions>(options =>
            //{
            //    options.DefaultResourceType = typeof(AbpExtendResource);
            //});
        }
    }
}
