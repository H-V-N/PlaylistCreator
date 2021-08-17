using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace SpotifyCache.Controllers
{
    public abstract class SpotifyCacheControllerBase: AbpController
    {
        protected SpotifyCacheControllerBase()
        {
            LocalizationSourceName = SpotifyCacheConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
