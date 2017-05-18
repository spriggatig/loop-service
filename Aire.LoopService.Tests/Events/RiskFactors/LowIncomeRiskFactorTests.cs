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
        private LowIncomeRiskFactor _lowIncomeRiskFactor;

        [SetUp]
        public void SetUp()
        {
            _lowIncomeRiskFactor = new LowIncomeRiskFactor();
        }

        [Test]
        public void Returns_True_WhenUnder_Threshold()
        {
            var application = Builder<Application>.CreateNew().With(_ => _.annual_inc = 2000).Build();

            var result = _lowIncomeRiskFactor.IsHighRisk(application);

            result.Should().BeTrue();
        }

        [Test]
        public void Returns_False_WhenOver_Threshold()
        {
            var application = Builder<Application>.CreateNew().With(_ => _.annual_inc = 10001).Build();

            var result = _lowIncomeRiskFactor.IsHighRisk(application);

            result.Should().BeFalse();
        }
    }
}
