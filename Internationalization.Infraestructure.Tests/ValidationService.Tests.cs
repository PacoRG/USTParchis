﻿using Infraestructure.Internationalization;
using Infraestructure.Internationalization.Json;
using Infraestructure.Tests.Utils;
using Infraestructure.Validation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;

namespace Infraestructure.Tests
{

    public class ValidatorServiceTests
    {
        private IStringLocalizerFactory CreateFactory()
        {
            IOptions<JsonLocalizationOptions> options = Options.Create(new JsonLocalizationOptions
            {
                ResourcePath = "Resources",
                SharedResourceName = "Shared"
            });

            IStringLocalizerFactory localizationFactory = new JsonStringLocalizerFactory(options);

            return localizationFactory;
        }
        [Fact]
        public void Should_Be_Invalid_On_DataAnnotation_Break()
        {
            var testModel = new TestValidationModel {
                RequiredProperty = null,
                StandardProperty = "StandardProperty",
                RequiredPropertyDefault = "RequiredPropertyDefault",
                CustomProperty ="Paco"
            };

            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.NotEmpty(validationMessages);
        }

        [Fact]
        public void Should_Set_Correctly_Identification_Base()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = null,
                StandardProperty = "StandardProperty",
                RequiredPropertyDefault = "RequiredPropertyDefault",
                CustomProperty = "Paco"
            };

            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("RequiredProperty",validationMessages.First().Identifier);
        }

        [Fact]
        public void Should_Be_Valid_On_No_Break()
        {
            var testModel = new TestValidationModel {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco",
                RequiredPropertyDefault= "RequiredPropertyDefault"
            };
            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Empty(validationMessages);
        }

        [Fact]
        public void Should_Be_Invalid_On_Custom_Validation_Break()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco2",
                RequiredPropertyDefault = "RequiredPropertyDefault"
            };

            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Single(validationMessages);
        }

        [Fact]
        public void Should_Validate_Using_Defined_Resources_Default()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco",
                RequiredPropertyDefault = null
            };

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");
            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("El campo Mi propiedad requerida por defecto es requerido", validationMessages.First().Message);
        }

        [Fact]
        public void Should_Validate_Using_Defined_Resources_Custom()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = null,
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco",
                RequiredPropertyDefault = "StandardProperty"
            };

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");
            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("El campo Mi propiedad requerida es custom requerido", validationMessages.First().Message);
        }

        [Fact]
        public void Should_Validate_Using_Entity_Resource()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = null,
                RequiredPropertyDefault = "RequiredPropertyDefault"
            };

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");
            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("El campo Mi propiedad custom tiene un error de validacion", validationMessages.First().Message);
        }

        [Fact]
        public void Should_Be_Invalid_On_Nested_Break()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco",
                RequiredPropertyDefault = "RequiredPropertyDefault",
                Nested = new TestValidationModelNested()
            };
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");

            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("El campo NestedProperty es requerido", validationMessages.First().Message);
        }

        [Fact]
        public void Should_Set_Identifier_Correctly_On_Nested()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco",
                RequiredPropertyDefault = "RequiredPropertyDefault",
                Nested = new TestValidationModelNested()
            };
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");

            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("Nested.NestedProperty", validationMessages.First().Identifier);
        }

        [Fact]
        public void Should_Be_Invalid_On_Nested_List_Break()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco",
                RequiredPropertyDefault = "RequiredPropertyDefault"
            };
            testModel.NestedList.Add(new TestValidationModelNested());

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");

            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("El campo NestedProperty es requerido", validationMessages.First().Message);
        }

        [Fact]
        public void Should_Set_Identifier_Correctly_On_Nested_List()
        {
            var testModel = new TestValidationModel
            {
                RequiredProperty = "RequiredProperty",
                StandardProperty = "StandardProperty",
                CustomProperty = "Paco",
                RequiredPropertyDefault = "RequiredPropertyDefault"
            };
            testModel.NestedList.Add(new TestValidationModelNested());

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");

            var sut = new ValidationService(this.CreateFactory());

            var validationMessages = sut.Validate(testModel);

            Assert.Equal("NestedList.NestedProperty", validationMessages.First().Identifier);
        }
    }
}
