using Application.API.Tests.Utils;
using Application.ViewModels;
using Application.ViewModels.Band;
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
            var game = new AuthorViewModel();

            var response = await testContext.Client.MakeRequestWithHeader(HttpMethod.Get, null, "/api/Culture");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<string>(responseString);
            Assert.Equal("This is a test", parsedResult);
        }

        [Fact]
        public async Task Should_Return_Name_Validation_On_DifferentCulture()
        {
            var testContext = new TestContext();
            var game = new AuthorViewModel();

            var response = await testContext.Client.MakeRequestWithHeader(HttpMethod.Get, null, "/api/Culture", "es");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<string>(responseString);
            Assert.Equal("Esto es una prueba", parsedResult);
        }

        [Fact]
        public async Task Should_Return_Name_Validation_On_DifferentCulture_WithCookie()
        {
            var testContext = new TestContext();

            var response = await testContext.Client.MakeRequestWithCookie(HttpMethod.Get, null, "/api/Culture","es");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<string>(responseString);
            Assert.Equal("Esto es una prueba", parsedResult);
        }
    }
}
