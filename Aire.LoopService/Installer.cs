using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Aire.LoopService.EventProcessors;
using Aire.LoopService.Events.RiskFactors;
using AutoMapper;
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
            container.Register(Component.For<IEventProcessor>().ImplementedBy<EventProcessor>());
            container.Register(Component.For<IIncreaseHighRisk>().ImplementedBy<IncreaseHighRisk>());
            container.Register(Component.For<ILowIncomeRiskFactor>().ImplementedBy<LowIncomeRiskFactor>());
            SetupAutoMapper(container);
        }

        private static void SetupAutoMapper(IWindsorContainer container)
        {
            var profileTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(Profile).IsAssignableFrom(type)).ToList();
            container?.Register(
              Component.For<IMapper>().UsingFactoryMethod(x =>
              {
                  return new MapperConfiguration(c =>
                  {
                      profileTypes.ForEach(c.AddProfile);
                  }).CreateMapper();
              }));
        }
    }
}