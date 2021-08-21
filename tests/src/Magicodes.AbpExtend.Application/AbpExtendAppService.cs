using System;
using System.Collections.Generic;
using System.Text;
using Magicodes.AbpExtend.Localization;
using Volo.Abp.Application.Services;

namespace Magicodes.AbpExtend
{
    /* Inherit your application services from this class.
     */
    public abstract class AbpExtendAppService : ApplicationService
    {
        protected AbpExtendAppService()
        {
            LocalizationResource = typeof(AbpExtendResource);
        }
    }
}
