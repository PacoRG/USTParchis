using Domain.Model;
using Domain.Persistence.Repositories;
using DomainServices.Services.Interfaces.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services.Band
{
    public class AuthorService : IAuthorService
    {
        private IGenericRepository<Author> _authorRepository;

        public AuthorService(IGenericRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ICollection<Author>> GetAll()
        {
            return await _authorRepository.GetAllAsyn();
        }
    }
}
