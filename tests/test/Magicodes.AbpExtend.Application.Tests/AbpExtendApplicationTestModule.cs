using Volo.Abp.Modularity;

namespace Magicodes.AbpExtend
{
    [DependsOn(
        typeof(AbpExtendApplicationModule),
        typeof(AbpExtendDomainTestModule)
        )]
    public class AbpExtendApplicationTestModule : AbpModule
    {

    }
}