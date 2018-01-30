using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Model.Infraestructure;

namespace DomainServices.Services.Interfaces.Domain
{
    public interface IAuthorService : IEntityService<Author>
    {
        Task<List<ValidationModel>> Save(Author author);
    }
}