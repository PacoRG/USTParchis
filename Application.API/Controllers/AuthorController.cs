using Application.API.Mapping;
using Application.ViewModels;
using Application.ViewModels.Band;
using Domain.Model;
using DomainServices.Interfaces.Infraestructure;
using DomainServices.Services.Interfaces.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Controllers
{
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
        public async Task<ICollection<AuthorViewModel>> GetAll()
        {
            _logger.LogInformation("Call to AuthorViemModel");

            var authors = await _authorService.GetAll();
            var authorsVM = authors.ToViewModel(_mapper);

            return authorsVM;
        }

        [HttpGet]
        public async Task<ICollection<AuthorViewModel>> GetPage(int pageNumber, int recordsPerPage)
        {
            var authors = await _authorService.GetPage(pageNumber, recordsPerPage);
            var authorsVM = authors.ToViewModel(_mapper);

            return authorsVM;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Call to DeleteAuthor");

            await _authorService.Delete(id);

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]AuthorViewModel authorVm)
        {
            _logger.LogInformation(authorVm.ToString());

            var author = _mapper.Map<Author>(authorVm);

            var validationResult = await _authorService.Save(author);
            var resultsViewModels = new List<ValidationResultViewModel>();

            foreach (var result in validationResult)
            {
                resultsViewModels.Add(_mapper.Map<ValidationResultViewModel>(result));
            }

            return this.Ok(resultsViewModels);
        }
    }
}
