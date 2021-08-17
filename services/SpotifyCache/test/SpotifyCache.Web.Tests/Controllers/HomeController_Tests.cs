using System.Threading.Tasks;
using SpotifyCache.Models.TokenAuth;
using SpotifyCache.Web.Controllers;
using Shouldly;
using Xunit;

namespace SpotifyCache.Web.Tests.Controllers
{
    public class HomeController_Tests: SpotifyCacheWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}