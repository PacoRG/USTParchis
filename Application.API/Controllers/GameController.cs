using Application.ViewModels;
using Domain.Model;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using Infraestructure.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IGameService _gameService;
        private IMapper _mapper;
        private IStringLocalizerFactory _localizerFactory;
        private IStringLocalizer _sharedLocalizer;
        private ILogger<GameController> _logger;

        public GameController(
            IGameService gameService,
            IMapper mapper,
            IStringLocalizerFactory localizerFactory,
            ILogger<GameController> logger)
        {
            _logger = logger;
            _gameService = gameService;
            _mapper = mapper;
            _localizerFactory = localizerFactory;
            _sharedLocalizer = _localizerFactory.Create(null);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GameViewModel gameVM)
        {
            _logger.LogInformation(gameVM.ToString());

            var game = _mapper.Map<Game>(gameVM);

            var validationResult = await _gameService.SaveGame(game);
            var resultsViewModels = new List<ValidationResultViewModel>();

            foreach(var result in validationResult)
            {
                resultsViewModels.Add(_mapper.Map<ValidationResultViewModel>(result));
            }

            return this.Ok(resultsViewModels);
        }
    }
}
