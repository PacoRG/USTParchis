using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainServices.Services.Interfaces
{
    public interface IValidationService
    {
        List<ValidationResult> Validate(object obj);
    }
}