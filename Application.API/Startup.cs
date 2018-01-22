using Application.API.Infraestructure;
using Infraestructure.API.Services;
using Infraestructure.Database;
using Infraestructure.Internationalization;
using Infraestructure.Internationalization.Json;
using Infraestructure.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Application.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var servcieRegister = new ServiceRegistryManager();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("USTParchis"), sqlOptions =>
                    sqlOptions.MigrationsAssembly("Infraestructure.Persistence")));

            services.AddMvc();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer), typeof(JsonStringLocalizer));

            services.Configure<JsonLocalizationOptions>(options => {
                options.ResourcePath = "Resources";
                options.SharedResourceName = "Shared";
            });

            servcieRegister.Register(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
