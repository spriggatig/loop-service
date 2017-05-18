using System.Threading.Tasks;
using Aire.LoopService.Domain;
using Aire.LoopService.EventProcessors;
using Aire.LoopService.Events;
using Aire.LoopService.Events.RiskFactors;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Aire.LoopService.Tests.EventProcessors
{
    [TestFixture]
    public class IncreaseHighRiskTests
    {
        private Mock<ILowIncomeRiskFactor> _mockLowIncomeRiskFactor;
        private IncreaseHighRisk _increaseHighRisk;

        [SetUp]
        public void SetUp()
        {
            _mockLowIncomeRiskFactor = new Mock<ILowIncomeRiskFactor>();
            _increaseHighRisk = new IncreaseHighRisk(_mockLowIncomeRiskFactor.Object);
        }

        [Test]
        public async Task Checks_Application_IsHighRisk()
        {
            var application = Builder<Application>.CreateNew().Build();
            _mockLowIncomeRiskFactor.Setup(_ => _.IsHighRisk(application)).Returns(true).Verifiable();

            await _increaseHighRisk.Process(application);

            _mockLowIncomeRiskFactor.Verify();
        }

        [Test]
        public async Task When_HighRisk_Adds_Application()
        {
            HighRiskEvents.Clear();
            _mockLowIncomeRiskFactor.Setup(_ => _.IsHighRisk(It.IsAny<Application>())).Returns(true).Verifiable();

            await _increaseHighRisk.Process(new Application());

            HighRiskEvents.Get().Count.Should().Be(1);
        }

        [Test]
        public async Task When_NotHighRisk_DoesNotAdd_Application()
        {
            HighRiskEvents.Clear();
            _mockLowIncomeRiskFactor.Setup(_ => _.IsHighRisk(It.IsAny<Application>())).Returns(false).Verifiable();

            await _increaseHighRisk.Process(new Application());

            HighRiskEvents.Get().Count.Should().Be(0);
        }
    }
}
