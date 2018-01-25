using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace DomainServices.Services.Interfaces.Domain
{
    public interface IAuthorService
    {
        Task<ICollection<Author>> GetAll();
    }
}