using Application.API.Mapping;
using Application.ViewModels.Band;
using DomainServices.Interfaces.Infraestructure;
using DomainServices.Services.Interfaces.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private IAuthorService _authorService;
        private IMapper _mapper;
        private ILogger<AuthorController> _logger;

        public AuthorController(
            IAuthorService authorService,
            IMapper mapper,
            ILogger<AuthorController> logger)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<AuthorViemModel>> Get()
        {
            _logger.LogInformation("Call to AuthorViemModel");

            var authors = await _authorService.GetAll();
            var authorsVM = authors.ToViewModel(_mapper);

            return authorsVM;
        }
    }
}
