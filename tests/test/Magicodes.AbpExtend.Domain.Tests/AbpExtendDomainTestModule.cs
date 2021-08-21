using Magicodes.AbpExtend.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Magicodes.AbpExtend
{
    [DependsOn(
        typeof(AbpExtendEntityFrameworkCoreTestModule)
        )]
    public class AbpExtendDomainTestModule : AbpModule
    {

    }
}