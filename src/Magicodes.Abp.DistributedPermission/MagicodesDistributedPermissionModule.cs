using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;

namespace Magicodes.Abp.DistributedPermission
{
    [DependsOn(
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule)
    )]
    public class MagicodesDistributedPermissionModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpSystemTextJsonSerializerOptions>(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            //目前仅支持中文
            using (CultureHelper.Use("zh-Hans"))
            {
                //启动时进行初始化
                var permissionDefinitionManager = context.ServiceProvider.GetService<IPermissionDefinitionManager>();
                permissionDefinitionManager.GetOrNull(string.Empty);
            }
            base.OnPostApplicationInitialization(context);
        }
    }
}
