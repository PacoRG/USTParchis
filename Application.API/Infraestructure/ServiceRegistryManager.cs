using Domain.Model;
using Domain.Persistence.Repositories;
using Domain.Services;
using DomainServices.Interfaces;
using DomainServices.Interfaces.Infraestructure;
using DomainServices.Services;
using DomainServices.Services.Interfaces;
using Infraestructure.API.Services;
using Infraestructure.Database;
using Infraestructure.Internationalization;
using Infraestructure.Internationalization.Json;
using Infraestructure.Persistence.Repositories;
using Infraestructure.Reflection;
using Infraestructure.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Application.API.Infraestructure
{
    public class ServiceRegistryManager
    {
        public void Register(IServiceCollection services)
        {
            RegisterDomain(services);
            RegisterInfraestructure(services);
        }

        public void ConfigureLocalization(IServiceCollection services)
        {
            services.Configure<JsonLocalizationOptions>(options =>
            {
                options.ResourcePath = "Resources";
                options.SharedResourceName = "Shared";
            });


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("es"),
                    new CultureInfo("en")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        private void RegisterDomain(IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IValidationService<Game>, GameValidator>();
        }

        private void RegisterInfraestructure(IServiceCollection services)
        {
            services.AddScoped<IValidationService, ValidationService>();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer), typeof(JsonStringLocalizer));
            services.AddScoped<DatabaseContext, DatabaseContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
