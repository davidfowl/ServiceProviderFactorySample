using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace ServiceProviderFactorySample
{
    public static class StructureMapExtensions
    {
        public static IServiceCollection AddStructureMap(this IServiceCollection services)
        {
            return services.AddSingleton<IServiceProviderFactory<IContainer>, StructureMapServiceProviderFactory>();
        }

        public static IWebHostBuilder UseStructureMap(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(services => services.AddStructureMap());
        }

        private class StructureMapServiceProviderFactory : IServiceProviderFactory<IContainer>
        {
            public IContainer CreateBuilder(IServiceCollection services)
            {
                var container = new Container();

                container.Populate(services);

                return container;
            }

            public IServiceProvider CreateServiceProvider(IContainer container)
            {
                return container.GetInstance<IServiceProvider>();
            }
        }
    }
}