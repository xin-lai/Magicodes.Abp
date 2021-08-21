using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Magicodes.Abp.Identity.Application
{
    [DependsOn(typeof(AbpIdentityApplicationModule))]
    public class MagicodesIdentityApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MagicodesIdentityApplicationModule>();
            });

            //Configure<AbpLocalizationOptions>(options =>
            //{
            //    options.DefaultResourceType = typeof(AbpExtendResource);
            //});
        }
    }
}
