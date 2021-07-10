using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NovelReader.API.Installers
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssemblies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment host, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var installers = assembly.ExportedTypes
                        .Where(x =>
                            typeof(IInstaller).IsAssignableFrom(x) &&
                            !x.IsInterface &&
                            !x.IsAbstract)
                        .Select(Activator.CreateInstance)
                        .Cast<IInstaller>()
                        .ToList();

                installers.ForEach(installer => installer.InstallServices(services, configuration, host));
            }
        }
    }
}
