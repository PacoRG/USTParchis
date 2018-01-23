using System.Collections.Generic;
using Domain.Model;
using DomainServices.Services.Interfaces;

namespace Domain.Services
{
    public class GameValidator : IValidationService<Game>
    {
        IValidationService _annotationValidator;

        public GameValidator(IValidationService annotationValidator)
        {
            _annotationValidator = annotationValidator;
        }

        public List<ValidationModel> Validate(Game obj)
        {
            var validationResult = _annotationValidator.Validate(obj);

            //My custom logic here

            return validationResult;
        }
    }
}
