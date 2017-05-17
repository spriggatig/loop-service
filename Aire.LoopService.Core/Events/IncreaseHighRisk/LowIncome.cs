using System;

namespace Aire.LoopService.Events.IncreaseHighRisk
{
    public class LowIncome : IHighRiskFactor
    {
        public bool IsHighRisk(ApplicationException application)
        {
            throw new NotImplementedException();
        }
    }
}
