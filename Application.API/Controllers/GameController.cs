using Application.ViewModels;
using Domain.Model;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public JsonResult Post([FromBody]GameViewModel gameVM)
        {
            var game = _mapper.Map<Game>(gameVM);

            var validationResult = _gameService.SaveGame(game);

            return this.Json(validationResult);
        }
    }
}
