using DomainServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainServices.Services
{
    public class ValidationService : IValidationService
    {
        public List<ValidationResult> Validate(object obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, context, validationResults, true);

            return validationResults;
        }
    }
}
