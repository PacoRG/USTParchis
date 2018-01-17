using Application.ViewModels;
using Domain.Model;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("parchisAPI/[controller]")]
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

        [HttpPost]
        public void Save(GameViewModel gameVM)
        {
            var game = _mapper.Map<Game>(gameVM);
            _gameService.SaveGame(game);
        }
    }
}
