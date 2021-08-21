using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Magicodes.AbpExtend.Web
{
    [Dependency(ReplaceServices = true)]
    public class AbpExtendBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AbpExtend";
    }
}
