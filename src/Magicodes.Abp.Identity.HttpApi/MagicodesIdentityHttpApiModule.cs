using Magicodes.Abp.Identity.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Magicodes.Abp.Identity.HttpApi
{
    [DependsOn(typeof(MagicodesIdentityApplicationModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class MagicodesIdentityHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MagicodesIdentityHttpApiModule).Assembly);
            });
        }
    }
}
