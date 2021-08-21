using Magicodes.AbpExtend.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Magicodes.AbpExtend.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AbpExtendController : AbpController
    {
        protected AbpExtendController()
        {
            LocalizationResource = typeof(AbpExtendResource);
        }
    }
}