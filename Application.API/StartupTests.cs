using Application.API.Infraestructure;
using Infraestructure.API.Services;
using Infraestructure.Database;
using Infraestructure.Internationalization;
using Infraestructure.Internationalization.Json;
using Infraestructure.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.API
{
    public class StartupTests
    {
        public StartupTests(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var servcieRegister = new ServiceRegistryManager();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase("MyTestDatabase"));

            services.AddLocalization();
            services.AddMvc();

            servcieRegister.Register(services);

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRequestLocalization();

            app.UseMvc();
        }
    }
}
