using System;

namespace Aire.LoopService.Events.IncreaseHighRisk
{
    public class JobTitle : IHighRiskFactor
    {
        public bool IsHighRisk(ApplicationException application)
        {
            throw new NotImplementedException();
        }
    }
}