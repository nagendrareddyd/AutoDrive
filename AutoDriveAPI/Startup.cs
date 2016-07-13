using AutoDriveAPI.IoC;
using AutoDriveServices;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(AutoDriveAPI.Startup))]
namespace AutoDriveAPI
{    
    public class Startup 
    {
        private static IWindsorContainer _container;
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();            
            app.UseWebApi(config);
            ConfigureWindsor(config);
            WebApiConfig.Register(config, _container);
            AutoMapperSetup.Init();            
        }

        private static void ConfigureWindsor(HttpConfiguration configuration)
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(_container);
            configuration.DependencyResolver = dependencyResolver;
        }
    }
}