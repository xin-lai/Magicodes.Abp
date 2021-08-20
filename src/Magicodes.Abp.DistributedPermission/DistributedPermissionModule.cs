using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Magicodes.Abp.DistributedPermission
{
    [DependsOn(
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule)
    )]
    public class DistributedPermissionModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpSystemTextJsonSerializerOptions>(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        }
    }
}
