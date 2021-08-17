using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SpotifyCache.Authorization;

namespace SpotifyCache
{
    [DependsOn(
        typeof(SpotifyCacheCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SpotifyCacheApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SpotifyCacheAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SpotifyCacheApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
