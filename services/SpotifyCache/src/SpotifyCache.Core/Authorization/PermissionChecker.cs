using Abp.Authorization;
using SpotifyCache.Authorization.Roles;
using SpotifyCache.Authorization.Users;

namespace SpotifyCache.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
