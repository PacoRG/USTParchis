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
    public class CultureControllerTests
    {
        public CultureControllerTests()
        {
        }

       

        [Fact]
        public async Task Should_Return_Name_Validation_On_DefaultCulture()
        {
            var testContext = new TestContext();
            var game = new GameViewModel();

            var response = await testContext.Client.MakeRequest(HttpMethod.Get, game, "/api/Culture");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<string>(responseString);
            Assert.Equal("This is a test", parsedResult);
        }

        [Fact]
        public async Task Should_Return_Name_Validation_On_DifferentCulture()
        {
            var testContext = new TestContext();
            var game = new GameViewModel();

            var response = await testContext.Client.MakeRequest(HttpMethod.Get, game, "/api/Culture", "es");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<string>(responseString);
            Assert.Equal("Esto es una prueba", parsedResult);
        }
    }
}
