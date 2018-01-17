using Domain.Model;
using Domain.Model.Enums;
using Domain.Persistence.Repositories;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainServices.Services
{

    public class GameService : IGameService
    {
        private IGenericRepository<Game> _gameRepository;

        public GameService(IGenericRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void SaveGame(Game game)
        {
            if (game == null) throw new NullReferenceException(nameof(game));

            if(GameExists(game))
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
        }

        private bool GameExists(Game game)
        {
            return _gameRepository.Get(game.Id) != null;
        }
    }
}
 