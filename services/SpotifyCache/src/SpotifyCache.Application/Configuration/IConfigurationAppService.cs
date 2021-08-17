using System.Threading.Tasks;
using SpotifyCache.Configuration.Dto;

namespace SpotifyCache.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
