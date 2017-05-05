using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StructureMap;
using StructureMap.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceProviderFactorySample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services here that have extension methods on IServiceCollection
        }

        public void ConfigureContainer(Registry config)
        {
            // Get rid of the nested closure for the user
            config.ForSingletonOf<IFoo>().Use<Foo>();
            
            // Use StructureMap's own type scanning functionality
            config.Scan(_ => {
                _.TheCallingAssembly();
                _.WithDefaultConventions();
            });
        }

        public void Configure(IApplicationBuilder app, Lazy<IFoo> foo)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello World! {foo.Value.GetHashCode()}");
            });
        }

        public class Foo : IFoo
        {

        }

        public interface IFoo
        {

        }
    }
}
