using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SpotifyCache.Configuration;

namespace SpotifyCache.Web.Host.Startup
{
    [DependsOn(
       typeof(SpotifyCacheWebCoreModule))]
    public class SpotifyCacheWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SpotifyCacheWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SpotifyCacheWebHostModule).GetAssembly());
        }
    }
}
