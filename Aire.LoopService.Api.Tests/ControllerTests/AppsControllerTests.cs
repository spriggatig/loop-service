using System.Linq;
using System.Threading.Tasks;
using Aire.LoopService.Api.Controllers;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Domain;
using AutoMapper;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace Aire.LoopService.Api.Tests.ControllerTests
{
    [TestFixture]
    public class AppsControllerTests
    {
        private AppsController _appsController;
        private Mock<IEventProcessor> _mockEventsProcessor;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void SetUp()
        {
            _mockEventsProcessor = new Mock<IEventProcessor>();
            _mockMapper = new Mock<IMapper>();
            _appsController = new AppsController(_mockEventsProcessor.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Maps_Model_To_Application()
        {
            var modelApps = Builder<AppModel>.CreateListOfSize(5).Build();

            await _appsController.Post(modelApps.ToArray());

            _mockMapper.Verify(_ => _.Map<Application>(It.IsAny<AppModel>()), Times.Exactly(5));
        }
    }
}
