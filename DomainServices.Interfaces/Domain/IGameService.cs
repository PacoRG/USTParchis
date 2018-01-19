using Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainServices.Interfaces
{
    public interface IGameService
    {
        List<ValidationResult> SaveGame(Game game);
    }
}
