using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SpotifyCache.EntityFrameworkCore;
using SpotifyCache.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace SpotifyCache.Web.Tests
{
    [DependsOn(
        typeof(SpotifyCacheWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class SpotifyCacheWebTestModule : AbpModule
    {
        public SpotifyCacheWebTestModule(SpotifyCacheEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SpotifyCacheWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(SpotifyCacheWebMvcModule).Assembly);
        }
    }
}