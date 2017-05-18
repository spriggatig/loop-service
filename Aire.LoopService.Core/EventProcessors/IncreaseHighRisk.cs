using System.Threading.Tasks;
using Aire.LoopService.Domain;
using Aire.LoopService.Events;
using Aire.LoopService.Events.RiskFactors;

namespace Aire.LoopService.EventProcessors
{
    public class IncreaseHighRisk : IIncreaseHighRisk
    {
        private readonly ILowIncomeRiskFactor _lowIncomeRiskFactor;

        public IncreaseHighRisk(ILowIncomeRiskFactor lowIncomeRiskFactor)
        {
            _lowIncomeRiskFactor = lowIncomeRiskFactor;
        }

        public Task Process(Application application)
        {
            var isLowIncomeHighRisk = _lowIncomeRiskFactor.IsHighRisk(application);
            if (isLowIncomeHighRisk)
            {
                HighRiskEvents.Add(application);
            }

            return Task.FromResult(0);
        }
    }
}
