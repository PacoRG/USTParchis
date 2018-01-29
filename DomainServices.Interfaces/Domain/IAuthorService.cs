using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace DomainServices.Services.Interfaces.Domain
{
    public interface IAuthorService
    {
        Task<ICollection<Author>> GetAll();

        Task Delete(int authorId);

        Task<List<ValidationModel>> Save(Author author);

        Task<ICollection<Author>> GetPage(int pageNumber, int recordsPerPage);

        Task<int> Count();
    }
}