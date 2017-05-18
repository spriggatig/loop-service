using Aire.LoopService.Domain;

namespace Aire.LoopService.Events.IncreaseHighRisk
{
    public interface ILowIncomeRiskFactor
    {
        bool IsHighRisk(Application application);
    }
}
