using Domain.Model;
using Domain.Persistence.Repositories;
using DomainServices.Services.Interfaces;
using DomainServices.Services.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services.Band
{
    public class AuthorService : IAuthorService
    {
        private IGenericRepository<Author> _authorRepository;
        private IValidationService _validationService;

        public AuthorService(
            IGenericRepository<Author> authorRepository,
            IValidationService validationService)
        {
            _authorRepository = authorRepository;
            _validationService = validationService;
        }

        public async Task<ICollection<Author>> GetAll()
        {
            return await _authorRepository.GetAllAsyn();
        }

        public async Task<ICollection<Author>> GetPage(int pageNumber, int recordsPerPage)
        {
            return await _authorRepository.GetPageAsyn(pageNumber, recordsPerPage);
        }

        public async Task Delete(int authorId)
        {
            if (await _authorRepository.GetAsync(authorId) == null)
            {
                throw new System.Exception($"The Author with Id : {authorId} does not exist.");
            }

            await _authorRepository.DeleteAsyn(authorId);
            await _authorRepository.SaveAsync();
            return;
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
                await _authorRepository.UpdateAsyn(author, author.Id);
            }
            else
            {
                await _authorRepository.AddAsyn(author);
            }

            await _authorRepository.SaveAsync();
            return new List<ValidationModel>();
        }

        private async Task<bool> AuthorExists(Author author)
        {
            return await _authorRepository.GetAsync(author.Id) != null;
        }
    }
}
