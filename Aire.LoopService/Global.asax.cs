using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using Aire.LoopService.Events;
using Castle.Windsor;

namespace Aire.LoopService.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new WindsorContainer();
            container.Install(new Installer());
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new ApiControllerActivator(container));
        }
    }
}
