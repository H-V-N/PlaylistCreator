using System.Threading.Tasks;
using Abp.Application.Services;
using SpotifyCache.Authorization.Accounts.Dto;

namespace SpotifyCache.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
