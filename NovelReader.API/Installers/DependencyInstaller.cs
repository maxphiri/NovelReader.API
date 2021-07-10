using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NovelReader.ApplicationService.Options;
using NovelReader.ApplicationService.Helpers;
using NovelReader.ApplicationService.Novels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovelReader.API.Installers
{
    public class DependencyInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration, IHostEnvironment host)
        {
            services.AddScoped<INovelsService, NovelsService>();
            services.AddScoped<IStringManager, StringManager>();

            services.Configure<SplitSettings>(Configuration.GetSection(nameof(SplitSettings)));

            services.AddControllersWithViews();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "NovelReader Api", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[0]}
                };
            });
        }
    }
}
