using Abp.Application.Services;
using SpotifyCache.MultiTenancy.Dto;

namespace SpotifyCache.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

