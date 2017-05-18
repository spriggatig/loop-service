using System.Threading.Tasks;
using Aire.LoopService.Domain;
using Aire.LoopService.EventProcessors;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace Aire.LoopService.Tests
{
    [TestFixture]
    public class EventProcessorTests
    {
        private Mock<IIncreaseHighRisk> _mockIncreaseHighRisk;
        private EventProcessor _eventProcessor;

        [SetUp]
        public void SetUp()
        {
            _mockIncreaseHighRisk = new Mock<IIncreaseHighRisk>();
            _eventProcessor = new EventProcessor(_mockIncreaseHighRisk.Object);
        }

        [Test]
        public async Task Processes_Application()
        {
            var application = Builder<Application>.CreateNew().Build();
            _mockIncreaseHighRisk.Setup(_ => _.Process(application)).Returns(Task.FromResult(0)).Verifiable();

            await _eventProcessor.Process(application);

            _mockIncreaseHighRisk.Verify();
        }
    }
}
