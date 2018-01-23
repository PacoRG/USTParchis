using Application.API.Tests.Utils;
using Application.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
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

            var response = await testContext.Client.MakeRequestWithHeader(HttpMethod.Post,game, "/api/Game");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(1, testContext.Database.Games.Count());
        }

        [Fact]
        public async Task Should_Return_Name_Validation_Error()
        {
            var testContext = new TestContext();
            var game = new GameViewModel();


            var response = await testContext.Client.MakeRequestWithHeader(HttpMethod.Post, game, "/api/Game");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<List<ValidationResultViewModel>>(responseString);

            Assert.Equal(0, testContext.Database.Games.Count());
            Assert.Equal("The field My Name is required", parsedResult.First().Message);
        }
    }
}
