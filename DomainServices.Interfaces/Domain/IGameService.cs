using Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainServices.Interfaces
{
    public interface IGameService
    {
        List<ValidationModel> SaveGame(Game game);
    }
}
