using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;
using SelfHostWebApp.Controller;
using SelfHostWebApp.Core;
using SelfHostWebApp.Model;
using SelfHostWebApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();

            config.Formatters.Add(new MultiPartMediaTypeFormatter());

            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var dirCatalog = new DirectoryCatalog(Environment.CurrentDirectory + @"\plugins");
            var aggregateCatalog = new AggregateCatalog(assemblyCatalog, dirCatalog);
            var container = new CompositionContainer(aggregateCatalog, true);

            builder.CreatePerOwinContext(() => new UserManager<MyUser>(new UserStore<MyUser>(new WebAppContext())));

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/api/auth"),
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthenticationProvider()
            };

            builder.UseOAuthAuthorizationServer(oAuthServerOptions);
            builder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            config.DependencyResolver = new DependencyResolver(container);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            builder.UseWebApi(config);
        }
    }
}
