using Domain.Model;
using Domain.Model.Enums;
using Domain.Persistence.Repositories;
using DomainServices.Interfaces;
using DomainServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainServices.Services
{

    public class GameService : IGameService
    {
        private IGenericRepository<Game> _gameRepository;
        private IValidationService _validationService;

        public GameService(
            IGenericRepository<Game> gameRepository,
            IValidationService validationService)
        {
            _gameRepository = gameRepository;
            _validationService = validationService;
        }

        public List<ValidationResult> SaveGame(Game game)
        {
            if (game == null)
                throw new NullReferenceException(nameof(game));

            var validationResult = _validationService.Validate(game);
            if (validationResult.Count > 0)
                return validationResult;

            if (GameExists(game))
            {
                game.ModifiedAt = DateTime.Now;
                _gameRepository.Update(game, game.Id);
            }
            else
            {
                var now = DateTime.Now;
                game.ModifiedAt = now;
                game.CreatedAt = now;
                game.State = GameState.InProgress;

                _gameRepository.Add(game);
            }

            _gameRepository.Save();
            return new List<ValidationResult>();
        }

        private bool GameExists(Game game)
        {
            return _gameRepository.Get(game.Id) != null;
        }
    }
}
