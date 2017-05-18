using System;
using Aire.LoopService.Domain;

namespace Aire.LoopService.Events.IncreaseHighRisk
{
    public class LowIncomeRiskFactor : ILowIncomeRiskFactor
    {
        public bool IsHighRisk(Application application)
        {
            throw new NotImplementedException();
        }
    }
}
