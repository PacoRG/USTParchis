using Infraestructure.API.Services;
using System.Globalization;
using Xunit;

namespace Infraestructure.Tests
{
    public class JsonStringLocalizerTests
    {
        private string sharedResource = "Shared";
        [Fact]
        public void Should_Retun_Key_On_Not_Existing_Resource()
        {
            var sut = new JsonStringLocalizer("Resources", sharedResource, new CultureInfo("en"));

            var localizedResult = sut["MyKey"];

            Assert.Equal("MyKey", localizedResult.Name);
            Assert.True(localizedResult.ResourceNotFound);
        }

        [Fact]
        public void Should_Retun_Translation_On_Correct()
        {
            var sut = new JsonStringLocalizer("Resources", "JsonTestEntity", new CultureInfo("es"));

            var localizedResult = sut["MyKey"];

            Assert.Equal("Mi valor", localizedResult.Value);
            Assert.False(localizedResult.ResourceNotFound);
        }

        [Fact]
        public void Should_Retun_Key_On_File_Existing_And_No_Key()
        {
            var sut = new JsonStringLocalizer("Resources", sharedResource, new CultureInfo("es"));

            var localizedResult = sut["MyKey2"];

            Assert.True(localizedResult.ResourceNotFound);
        }

        [Fact]
        public void Should_Retun_Paremetrized_Value()
        {
            var sut = new JsonStringLocalizer("Resources", "JsonTestEntity", new CultureInfo("es"));

            var localizedResult = sut["ParamKey", new object[] { "1"}];

            Assert.Equal("MyValor1", localizedResult.Value);
            Assert.False(localizedResult.ResourceNotFound);
        }

        [Fact]
        public void Should_Retun_All_Keys()
        {
            var sut = new JsonStringLocalizer("Resources", "JsonTestEntity",  new CultureInfo("es"));

            var localizer = sut.WithCulture(new CultureInfo("es"));
            var localized = localizer["MyKey"];

            Assert.Equal("Mi valor", localized.Value);
            Assert.False(localized.ResourceNotFound);
        }
    }
}
