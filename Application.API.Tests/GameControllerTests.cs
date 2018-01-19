using Application.API.Tests.Utils;
using Application.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.API.Tests
{
    public class GameControllerTests
    {
        public GameControllerTests()
        {
        }

        [Fact]
        public async Task Should_Save_Game()
        {
            var testContext = new TestContext();
            var game = new GameViewModel { Name = "MyName" };

            var response = await testContext.Client.PostAsJsonAsync(game, "/api/Game");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(1, testContext.Database.Games.Count());
        }

        [Fact]
        public async Task Should_Return_Validation_Errors()
        {
            var testContext = new TestContext();
            var game = new GameViewModel();

            var response = await testContext.Client.PostAsJsonAsync(game, "/api/Game");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(0, testContext.Database.Games.Count());
        }
    }
}
