using Domain.Model;
using Domain.Model.Infraestructure;
using Domain.Persistence.Repositories;
using DomainServices.Services.Base;
using DomainServices.Services.Interfaces;
using DomainServices.Services.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services.Band
{
    public class AuthorService : EntityService<Author>, IAuthorService
    {
        private IValidationService _validationService;

        public AuthorService(
            IGenericRepository<Author> repository,
            IValidationService validationService): base(repository)
        {
            _validationService = validationService;
        }

        public async Task<List<ValidationModel>> Save(Author author)
        {
            if (author == null)
                throw new NullReferenceException(nameof(author));

            var validationResult = _validationService.Validate(author);
            if (validationResult.Count > 0)
                return validationResult;

            var authorExists = await AuthorExists(author);

            if (authorExists)
            {
                await _repository.UpdateAsyn(author, author.Id);
            }
            else
            {
                await _repository.AddAsyn(author);
            }

            await _repository.SaveAsync();
            return new List<ValidationModel>();
        }

        private async Task<bool> AuthorExists(Author author)
        {
            return await _repository.GetAsync(author.Id) != null;
        }
    }
}
