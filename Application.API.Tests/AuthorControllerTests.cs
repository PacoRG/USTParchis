using Application.API.Tests.Utils;
using Application.ViewModels.Band;
using Domain.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Application.API.Tests
{
    public class AuthorControllerTests
    {
        private TestContext _testContext;

        public AuthorControllerTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Should_Get_Authors()
        {
            var author = new Author { Name = "MyName" };
            _testContext.Database.Add(author);
            _testContext.Database.SaveChanges();

            var response = await _testContext.Client.MakeRequestWithHeader(HttpMethod.Get,"", "/api/Author");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<ICollection<AuthorViemModel>>(responseString);

            Assert.Single(parsedResult);
            Assert.Equal("MyName", parsedResult.First().Name);
        }
    }
}
