using Domain.Model;
using Domain.Model.Enums;
using Domain.Persistence.Repositories;
using DomainServices.Interfaces;
using DomainServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Services
{

    public class GameService : IGameService
    {
        private IGenericRepository<Game> _gameRepository;
        private IValidationService<Game> _validationService;

        public GameService(
            IGenericRepository<Game> gameRepository,
            IValidationService<Game> validationService)
        {
            _gameRepository = gameRepository;
            _validationService = validationService;
        }

        public async Task<List<ValidationModel>> SaveGame(Game game)
        {
            if (game == null)
                throw new NullReferenceException(nameof(game));

            var validationResult = _validationService.Validate(game);
            if (validationResult.Count > 0)
                return validationResult;

            var gameExists = await GameExists(game);

            if (gameExists)
            {
                game.ModifiedAt = DateTime.Now;
                await _gameRepository.UpdateAsyn(game, game.Id);
            }
            else
            {
                var now = DateTime.Now;
                game.ModifiedAt = now;
                game.CreatedAt = now;
                game.State = GameState.InProgress;

                await _gameRepository.AddAsyn(game);
            }

            await _gameRepository.SaveAsync();
            return new List<ValidationModel>();
        }

        private async Task<bool> GameExists(Game game)
        {
            return await _gameRepository.GetAsync(game.Id) != null;
        }
    }
}
