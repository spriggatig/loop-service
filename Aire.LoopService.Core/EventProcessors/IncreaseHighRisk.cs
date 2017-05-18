using System;
using System.Threading.Tasks;
using Aire.LoopService.Domain;
using Aire.LoopService.Events.IncreaseHighRisk;

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
            //// if isLowIncomeHighRisk is true then add to list of high risk applications
            throw new NotImplementedException();
        }
    }
}
