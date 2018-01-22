using Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainServices.Services.Interfaces
{
    public interface IValidationService
    {
        List<ValidationModel> Validate(object obj);
    }
}