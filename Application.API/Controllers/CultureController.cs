using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Application.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Culture")]
    public class CultureController : Controller
    {
        private IStringLocalizerFactory _localizerFactory;
        private IStringLocalizer _sharedLocalizer;

        public CultureController(
            IStringLocalizerFactory localizerFactory)
        {
            _localizerFactory = localizerFactory;
            _sharedLocalizer = _localizerFactory.Create(null);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _sharedLocalizer["MyTestString"];
            return this.Ok(result.Value);
        }
    }
}