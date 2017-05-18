using Aire.LoopService.Domain;
using Aire.LoopService.Events.RiskFactors;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Aire.LoopService.Tests.Events.RiskFactors
{
    [TestFixture]
    public class LowIncomeRiskFactorTests
    {
        private int _lowIncomeThreshold;
        private LowIncomeRiskFactor _lowIncomeRiskFactor;

        [SetUp]
        public void SetUp()
        {
            _lowIncomeThreshold = 5000;
            _lowIncomeRiskFactor = new LowIncomeRiskFactor(_lowIncomeThreshold);
        }

        [Test]
        public void Returns_True_WhenUnder_Threshold()
        {
            var application = Builder<Application>.CreateNew().With(_ => _.annual_inc = _lowIncomeThreshold - 1).Build();

            var result = _lowIncomeRiskFactor.IsHighRisk(application);

            result.Should().BeTrue();
        }

        [Test]
        public void Returns_False_WhenOver_Threshold()
        {
            var application = Builder<Application>.CreateNew().With(_ => _.annual_inc = _lowIncomeThreshold + 1).Build();

            var result = _lowIncomeRiskFactor.IsHighRisk(application);

            result.Should().BeFalse();
        }

        [Test]
        public void Returns_False_WhenEqual_Threshold()
        {
            var application = Builder<Application>.CreateNew().With(_ => _.annual_inc = _lowIncomeThreshold).Build();

            var result = _lowIncomeRiskFactor.IsHighRisk(application);

            result.Should().BeFalse();
        }
    }
}
