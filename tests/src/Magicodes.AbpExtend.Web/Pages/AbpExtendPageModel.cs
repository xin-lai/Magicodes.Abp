using Magicodes.AbpExtend.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Magicodes.AbpExtend.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class AbpExtendPageModel : AbpPageModel
    {
        protected AbpExtendPageModel()
        {
            LocalizationResourceType = typeof(AbpExtendResource);
        }
    }
}