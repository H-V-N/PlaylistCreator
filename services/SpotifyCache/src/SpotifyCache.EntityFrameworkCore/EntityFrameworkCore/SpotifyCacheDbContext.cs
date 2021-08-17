using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using SpotifyCache.Authorization.Roles;
using SpotifyCache.Authorization.Users;
using SpotifyCache.MultiTenancy;

namespace SpotifyCache.EntityFrameworkCore
{
    public class SpotifyCacheDbContext : AbpZeroDbContext<Tenant, Role, User, SpotifyCacheDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public SpotifyCacheDbContext(DbContextOptions<SpotifyCacheDbContext> options)
            : base(options)
        {
        }
    }
}
