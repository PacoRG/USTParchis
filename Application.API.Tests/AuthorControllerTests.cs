using Application.API.Tests.Utils;
using Application.ViewModels;
using Application.ViewModels.Band;
using Domain.Model;
using Domain.Model.Infraestructure;
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

            var response = await _testContext.Client.MakeRequestWithHeader(HttpMethod.Get, "", "/Author/GetAll");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<ICollection<AuthorViewModel>>(responseString);

            Assert.Single(parsedResult);
            Assert.Equal("MyName", parsedResult.First().Name);
        }

        [Fact]
        public async Task Should_Delete_Author()
        {
            var author = new Author { Name = "MyName" };
            _testContext.Database.Add(author);
            _testContext.Database.SaveChanges();

            var response = await _testContext.Client.MakeRequestWithHeader(
                HttpMethod.Post,
                "",
                "/Author/Delete/" + author.Id);

            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task Should_Save_Author()
        {
            var testContext = new TestContext();
            var author = new AuthorViewModel { Name = "MyName" };

            var response = await testContext.Client.MakeRequestWithHeader(HttpMethod.Post, author, "/Author/Save");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(1, testContext.Database.Authors.Count());
        }

        [Fact]
        public async Task Should_Return_Validation_Error_On_Save()
        {
            var testContext = new TestContext();
            var game = new AuthorViewModel();


            var response = await testContext.Client.MakeRequestWithHeader(HttpMethod.Post, game, "/Author/Save");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<List<ValidationResultViewModel>>(responseString);

            Assert.Equal(0, testContext.Database.Authors.Count());
            Assert.NotEmpty(parsedResult);
        }

        [Fact]
        public async Task Should_Get_Paginated_Authors()
        {
            for (int i = 0; i < 10; i++)
            {
                var author = new Author { Name = "Author" + i };
                _testContext.Database.Add(author);
            }
            _testContext.Database.SaveChanges();

            var searchModel = new SearchModel
            {
                PageIndex = 2,
                RecordsPerPage = 3
            };

            var query = $"/Author/GetPage";
            var response = await _testContext.Client.MakeRequestWithHeader(HttpMethod.Post, searchModel, query);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<SearchResultModel<AuthorViewModel>>(responseString);

            Assert.Equal(3, parsedResult.Records.Count);
            Assert.Equal("Author3", parsedResult.Records.First().Name);
        }

        [Fact]
        public async Task Should_Get_Sorted_Authors()
        {
            for (int i = 0; i < 10; i++)
            {
                var author = new Author { Name = "Author" + i };
                _testContext.Database.Add(author);
            }
            _testContext.Database.SaveChanges();

            var searchModel = new SearchModel
            {
                SortColumn = "Name",
                IsAscendingSort = false
            };

            var query = $"/Author/GetPage";
            var response = await _testContext.Client.MakeRequestWithHeader(HttpMethod.Post, searchModel, query);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<SearchResultModel<AuthorViewModel>>(responseString);

            Assert.Equal("Author9", parsedResult.Records.First().Name);
        }

        [Fact]
        public async Task Should_Get_Filtered_Authors()
        {
            for (int i = 0; i < 10; i++)
            {
                var author = new Author { Name = "Author" + i };
                _testContext.Database.Add(author);
            }
            _testContext.Database.SaveChanges();

            var searchModel = new SearchModel();
            searchModel.Filters.Add(
                new FilterModel
                {
                    Column = "Name",
                    FilterValue = "2",
                    Type = FilterType.Contains
                });

            var query = $"/Author/GetPage";
            var response = await _testContext.Client.MakeRequestWithHeader(HttpMethod.Post, searchModel, query);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<SearchResultModel<AuthorViewModel>>(responseString);

            Assert.Equal("Author2", parsedResult.Records.First().Name);
        }

        [Fact]
        public async Task Should_Get_Filtered_On_Integer_Authors()
        {
            for (int i = 0; i < 10; i++)
            {
                var author = new Author { Name = "Author" + i };
                _testContext.Database.Add(author);
            }
            _testContext.Database.SaveChanges();

            var searchModel = new SearchModel();
            searchModel.Filters.Add(
                new FilterModel
                {
                    Column = "Id",
                    FilterValue = "3",
                    Type = FilterType.Contains
                });

            var query = $"/Author/GetPage";
            var response = await _testContext.Client.MakeRequestWithHeader(HttpMethod.Post, searchModel, query);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<SearchResultModel<AuthorViewModel>>(responseString);

            Assert.Equal("Author2", parsedResult.Records.First().Name);
        }

        [Fact]
        public async Task Should_Count()
        {
            for (int i = 0; i < 10; i++)
            {
                var author = new Author { Name = "Author" + i };
                _testContext.Database.Add(author);
            }
            _testContext.Database.SaveChanges();

            var query = $"/Author/Count";
            var response = await _testContext.Client.MakeRequestWithHeader(HttpMethod.Get, "", query);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var parsedResult = JsonConvert.DeserializeObject<int>(responseString);

            Assert.Equal(10, parsedResult);
        }

    }
}
