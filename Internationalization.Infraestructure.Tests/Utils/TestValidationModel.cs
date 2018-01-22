using Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Tests.Utils
{
    [DataEntity]
    public class TestValidationModel
    {
        public TestValidationModelNested Nested { get; set; }

        [Required(ErrorMessage = "CustomRequiredError")]
        public string RequiredProperty { get; set; }

        [Required]
        public string RequiredPropertyDefault { get; set; }

        [Custom(ErrorMessage = "CustomError")]
        public string CustomProperty { get; set; }

        public string StandardProperty { get; set; }
    }
}
