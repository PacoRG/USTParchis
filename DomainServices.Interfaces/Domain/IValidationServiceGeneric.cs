using Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainServices.Services.Interfaces
{
    public interface IValidationService<T>
    {
        List<ValidationModel> Validate(T obj);
    }
}