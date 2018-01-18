using Domain.Services.Tests.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Services.Tests
{
    public class TestValidationModel
    {
        [Required]
        [Custom]
        public string RequiredProperty { get; set; }

        public string StandardProperty { get; set; }
    }
}
