using Application.ViewModels;
using Domain.Model;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IGameService _gameService;
        private IMapper _mapper;

        public GameController(
            IGameService gameService,
            IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var gameVM = new GameViewModel();
            gameVM.Name = "MyGame";

            var game = _mapper.Map<Game>(gameVM);
            _gameService.SaveGame(game);

            return null;
        }
    }
}
