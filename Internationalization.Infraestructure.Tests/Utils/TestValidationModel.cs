using Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Tests.Utils
{
    [DataEntity]
    public class TestValidationModel
    {
        public TestValidationModel()
        {
            NestedList = new List<TestValidationModelNested>();
        }

        public TestValidationModelNested Nested { get; set; }

        public IList<TestValidationModelNested> NestedList { get; set; }

        [Required(ErrorMessage = "CustomRequiredError")]
        public string RequiredProperty { get; set; }

        [Required]
        public string RequiredPropertyDefault { get; set; }

        [Custom(ErrorMessage = "CustomError")]
        public string CustomProperty { get; set; }

        public string StandardProperty { get; set; }
    }
}
