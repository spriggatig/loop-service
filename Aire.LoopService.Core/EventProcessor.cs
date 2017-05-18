using System.Threading.Tasks;
using Aire.LoopService.Domain;
using Aire.LoopService.Events.IncreaseHighRisk;

namespace Aire.LoopService
{
    public class EventProcessor : IEventProcessor
    {
        private readonly ILowIncomeRiskFactor _lowIncomeRiskFactor;

        public EventProcessor(ILowIncomeRiskFactor lowIncomeRiskFactor)
        {
            _lowIncomeRiskFactor = lowIncomeRiskFactor;
        }

        public Task Process(Application application)
        {
            var isHighRisk = _lowIncomeRiskFactor.IsHighRisk(application);
            throw new System.NotImplementedException();
        }
    }
}