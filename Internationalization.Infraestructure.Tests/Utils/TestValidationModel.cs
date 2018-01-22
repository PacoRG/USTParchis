using Domain.Services.Tests.Utils;
using System.ComponentModel.DataAnnotations;

namespace Domain.Services.Tests
{
    public class TestValidationModel
    {
        [Required(ErrorMessage = "CustomRequiredError")]
        public string RequiredProperty { get; set; }

        [Required]
        public string RequiredPropertyDefault { get; set; }

        [Custom(ErrorMessage = "CustomError")]
        public string CustomProperty { get; set; }

        public string StandardProperty { get; set; }
    }
}
