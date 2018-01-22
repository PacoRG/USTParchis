using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infraestructure.Validation
{
    public class CustomValidationMetadataProvider : IMetadataDetailsProvider
    {
        Dictionary<Type, string> _errorMessagesMap;

        public CustomValidationMetadataProvider()
        {
            _errorMessagesMap = new Dictionary<Type, string>
            {
                {typeof(RequiredAttribute), "RequiredError" },
                {typeof(MaxLengthAttribute), "MaxLengthError" }
            };
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            foreach(object attribute in context.Attributes)
            {
                var validationAttribute = attribute as ValidationAttribute;

                var type = attribute.GetType();

                if (_errorMessagesMap.TryGetValue(type, out string key))
                {
                    validationAttribute.ErrorMessage = key;
                }
            }
        }
    }
}
