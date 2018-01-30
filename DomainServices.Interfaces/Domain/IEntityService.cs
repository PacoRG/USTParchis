using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Model.Infraestructure;

namespace DomainServices.Services.Interfaces.Domain
{
    public interface IEntityService<T> where T:class
    {
        Task<ICollection<T>> GetAll();

        Task Delete(int authorId);

        Task<SearchResultModel<T>> GetPage(SearchModel searchModel);

        Task<int> Count();
    }
}