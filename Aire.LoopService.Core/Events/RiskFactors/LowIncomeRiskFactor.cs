using Aire.LoopService.Domain;

namespace Aire.LoopService.Events.RiskFactors
{
    public class LowIncomeRiskFactor : ILowIncomeRiskFactor
    {
        private readonly int _lowIncomeThreshold;

        public LowIncomeRiskFactor(int lowIncomeThreshold)
        {
            _lowIncomeThreshold = lowIncomeThreshold;
        }

        public bool IsHighRisk(Application application)
        {
            // Extra login could be implemented here to determin whether the application is below the low income theshold
            return application.annual_inc < _lowIncomeThreshold;
        }
    }
}
