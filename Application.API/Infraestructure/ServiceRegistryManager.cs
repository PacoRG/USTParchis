using Domain.Persistence.Repositories;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using DomainServices.Services;
using Infraestructure.Persistence.Repositories;
using KPMG.Tax.Portal.TaxRay.Mappings;
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
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
