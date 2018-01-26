using Application.ViewModels.Band;
using Domain.Model;
using DomainServices.Interfaces.Infraestructure;
using System.Collections.Generic;

namespace Application.API.Mapping
{
    public static class AuthorMapping
    {
        public static ICollection<AuthorViewModel> ToViewModel(this ICollection<Author> authors, IMapper mapper)
        {
            var authorsVM = new List<AuthorViewModel>();

            foreach(var author in authors)
            {
                authorsVM.Add(mapper.Map<AuthorViewModel>(author));
            }

            return authorsVM;
        }

        public static ICollection<Author> ToDomain(this ICollection<AuthorViewModel> authors, IMapper mapper)
        {
            var authorsVM = new List<Author>();

            foreach (var author in authors)
            {
                authorsVM.Add(mapper.Map<Author>(author));
            }

            return authorsVM;
        }
    }
}
