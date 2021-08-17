using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using SpotifyCache.Configuration.Dto;

namespace SpotifyCache.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SpotifyCacheAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
