using DomainServices.Services;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Domain.Services.Tests
{

    public class ValidatorServiceTests
    {
        private Mock<IStringLocalizer> BuildLoclaizer()
        {
            var localizedString = new LocalizedString("Test", "MyStringValue");

            var stringLocalizer = new Mock<IStringLocalizer>();

            stringLocalizer.Setup(x => x[It.IsAny<string>()])
                .Returns(localizedString)
                .Verifiable();

            return stringLocalizer;
        }

        [Fact]
        public void Should_Be_Invalid_On_DataAnnotation_Break()
        {
            var testModel = new TestValidationModel { RequiredProperty = null, StandardProperty = "MyProperty" };
            var sut = new ValidationService();

            var validationMessages = sut.Validate(testModel);

            Assert.NotEmpty(validationMessages);
        }

        [Fact]
        public void Should_Be_Valid_On_No_Break()
        {
            var testModel = new TestValidationModel { RequiredProperty = "Paco", StandardProperty = "MyProperty" };
            var sut = new ValidationService();

            var validationMessages = sut.Validate(testModel);

            Assert.Empty(validationMessages);
        }

        [Fact]
        public void Should_Be_Invalid_On_Custom_Validation_Break()
        {
            var testModel = new TestValidationModel { RequiredProperty = "Required", StandardProperty = "MyProperty" };
            var sut = new ValidationService();

            var validationMessages = sut.Validate(testModel);

            Assert.Single(validationMessages);
        }
    }
}
