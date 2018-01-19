using Infraestructure.Internationalization;
using Infraestructure.Internationalization.Json;
using Microsoft.Extensions.Options;
using System.Globalization;
using Xunit;

namespace Internationalization.Infraestructure.Tests
{
    public class JsonStringLocalizerFactoryTests
    {
        private string sharedResource = "Shared";

        public string SharedResource { get => sharedResource; set => sharedResource = value; }

        [Fact]
        public void Should_Retun_Shared_Localizer_On_Null()
        {
            var someOptions = Options.Create(new JsonLocalizationOptions() {
                ResourcePath = "Resources",
                SharedResourceName = "Shared"
            });

            CultureInfo.CurrentUICulture = new CultureInfo("es");
            var sut = new JsonStringLocalizerFactory(someOptions);

            var localizer = sut.Create(null);
            var result = localizer["MyKey"];

            Assert.True(!result.ResourceNotFound);
        }

        [Fact]
        public void Should_Retun_Shared_Localizer_On_Type()
        {
            var someOptions = Options.Create(new JsonLocalizationOptions()
            {
                ResourcePath = "Resources",
                SharedResourceName = "Shared"
            });

            CultureInfo.CurrentUICulture = new CultureInfo("es");
            var sut = new JsonStringLocalizerFactory(someOptions);

            var localizer = sut.Create(typeof(JsonTestEntity));
            var result = localizer["MyKeyJson"];

            Assert.True(!result.ResourceNotFound);
        }
    }
}
