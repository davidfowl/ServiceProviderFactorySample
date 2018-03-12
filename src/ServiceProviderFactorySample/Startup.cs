using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using StructureMap.Pipeline;

namespace ServiceProviderFactorySample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services here that have extension methods on IServiceCollection
        }

        public void ConfigureContainer(IContainer container)
        {
            container.Configure(config =>
            {
                config.For<IFoo>().Use<Foo>().SetLifecycleTo<SingletonLifecycle>();
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
