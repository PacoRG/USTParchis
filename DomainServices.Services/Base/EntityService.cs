using Domain.Model.Infraestructure;
using Domain.Persistence.Repositories;
using DomainServices.Services.Interfaces.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services.Base
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        protected IGenericRepository<T> _repository;

        public EntityService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _repository.GetAllAsyn();
        }

        public async Task<SearchResultModel<T>> GetPage(SearchModel searchModel)
        {
            return await _repository.SearchAsync(searchModel);
        }

        public async Task<int> Count()
        {
            return await _repository.CountAsync();
        }

        public async Task Delete(int authorId)
        {
            if (await _repository.GetAsync(authorId) == null)
            {
                throw new System.Exception($"The Author with Id : {authorId} does not exist.");
            }

            await _repository.DeleteAsyn(authorId);
            await _repository.SaveAsync();
            return;
        }
    }
}
