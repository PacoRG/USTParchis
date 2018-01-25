using Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IGameService
    {
        Task<List<ValidationModel>> SaveGame(Game game);
    }
}
