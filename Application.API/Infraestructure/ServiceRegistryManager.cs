using Domain.Persistence.Repositories;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using DomainServices.Services;
using Infraestructure.Database;
using Infraestructure.Persistence.Repositories;
using Infraestructure.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace Application.API.Infraestructure
{
    public class ServiceRegistryManager
    {
        public void Register(IServiceCollection services)
        {
            RegisterDomain(services);
            RegisterInfraestructure(services);
        }
        
        private void RegisterDomain(IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
        }

        private void RegisterInfraestructure(IServiceCollection services)
        {
            services.AddScoped<DatabaseContext, DatabaseContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
