using Aire.LoopService.Domain;

namespace Aire.LoopService.Events.RiskFactors
{
    public interface ILowIncomeRiskFactor
    {
        bool IsHighRisk(Application application);
    }
}
