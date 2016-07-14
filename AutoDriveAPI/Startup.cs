using AutoDriveAPI.IoC;
using AutoDriveServices;
using AutoDriveServices.MongoIdentity;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
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
			ConfigureOAuthTokenGeneration(app);
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
		private void ConfigureOAuthTokenGeneration(IAppBuilder app)
		{
			// Configure the db context and user manager to use a single instance per request			
			app.CreatePerOwinContext(ApplicationIdentityContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
			// Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here

			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

		}
	}
}