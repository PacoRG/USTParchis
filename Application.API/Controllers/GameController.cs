using Application.ViewModels;
using Domain.Model;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using Infraestructure.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IGameService _gameService;
        private IMapper _mapper;
        private IStringLocalizerFactory _localizerFactory;
        private IStringLocalizer _sharedLocalizer;

        public GameController(
            IGameService gameService,
            IMapper mapper,
            IStringLocalizerFactory localizerFactory)
        {
            _gameService = gameService;
            _mapper = mapper;
            _localizerFactory = localizerFactory;
            _sharedLocalizer = _localizerFactory.Create(null);
        }

        [HttpPost]
        public IActionResult Post([FromBody]GameViewModel gameVM)
        {
            var game = _mapper.Map<Game>(gameVM);

            var c1 = CultureInfo.CurrentCulture;
            var c2 = CultureInfo.CurrentUICulture;

            var validationResult = _gameService.SaveGame(game);
            var resultsViewModels = new List<ValidationResultViewModel>();

            foreach(var result in validationResult)
            {
                resultsViewModels.Add(_mapper.Map<ValidationResultViewModel>(result));
            }

            return this.Ok(resultsViewModels);
        }
    }
}
