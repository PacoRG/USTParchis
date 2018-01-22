using Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Tests.Utils
{
    [DataEntity]
    public class TestValidationModelNested
    {
        [Required]
        public string NestedProperty { get; set; }
    }
}
