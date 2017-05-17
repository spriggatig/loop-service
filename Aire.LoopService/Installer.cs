using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Aire.LoopService.Events.IncreaseHighRisk;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Aire.LoopService.Api
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null)
            {
                return;
            }

            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient());
            container.Register(Classes.FromAssembly(Assembly.Load("Aire.LoopService"))
                .BasedOn<IEventProcessor>()
                .WithService.AllInterfaces()
                .LifestyleTransient());
            container.Register(Classes.FromAssembly(Assembly.Load("Aire.LoopService"))
                .BasedOn<IHighRiskFactor>()
                .WithService.AllInterfaces()
                .LifestyleTransient());
        }
    }
}