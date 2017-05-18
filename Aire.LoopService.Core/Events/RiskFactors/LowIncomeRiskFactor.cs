using System;
using Aire.LoopService.Domain;
using Aire.LoopService.Events.RiskFactors;

namespace Aire.LoopService.Events.RiskFactors
{
    public class LowIncomeRiskFactor : ILowIncomeRiskFactor
    {
        public bool IsHighRisk(Application application)
        {
            return application.annual_inc < 10000;
        }
    }
}
