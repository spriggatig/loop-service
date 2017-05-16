using System;

namespace Aire.LoopService.Events.IncreaseHighRisk
{
    public interface IHighRiskFactor
    {
        bool IsHighRisk(ApplicationException application);
    }
}
