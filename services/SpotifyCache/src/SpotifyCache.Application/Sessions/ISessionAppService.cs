using System.Threading.Tasks;
using Abp.Application.Services;
using SpotifyCache.Sessions.Dto;

namespace SpotifyCache.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
