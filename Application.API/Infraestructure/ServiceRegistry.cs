using Domain.Persistence.Repositories;
using Infraestructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace Application.API.Infraestructure
{
    public class ServiceRegistry
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
