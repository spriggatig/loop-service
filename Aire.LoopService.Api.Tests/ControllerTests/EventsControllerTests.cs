using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aire.LoopService.Api.Controllers;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Domain;
using Aire.LoopService.Events;
using FluentAssertions;
using NUnit.Framework;
using FizzWare.NBuilder;
using Moq;
using System;

namespace Aire.LoopService.Api.Tests.ControllerTests
{
    [TestFixture]
    public class EventsControllerTests
    {
        private Mock<IClock> _mockClock;
        private Mock<IThresholdProvider> _mockThresholdProvider;
        private EventsController _eventsController;

        [SetUp]
        public void SetUp()
        {
            _mockClock = new Mock<IClock>();
            _mockThresholdProvider = new Mock<IThresholdProvider>();
            _eventsController = new EventsController(_mockClock.Object, _mockThresholdProvider.Object);
            ApplicationHistory.Clear();
            HighRiskEvents.Clear();
        }

        [Test]
        public async Task Returns_HighRiskEvent()
        {
            var endDate = new DateTime(2017, 01, 05);
            for (var i = 0; i < 100; i++)
            {
                ApplicationHistory.Add(new Application { timestamp = endDate.AddMinutes(-10) });
            }

            for (var i = 0; i < 6; i++)
            {
                HighRiskEvents.Add(new Application { timestamp = endDate.AddMinutes(-10) } );
            }

            _mockClock.Setup(_ => _.Now).Returns(endDate);

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCREASE_HIGH_RISK").Should().NotBeNull();
        }

        [Test]
        public async Task DoesNot_Return_HighRiskEvent()
        {
            var endDate = new DateTime(2017, 04, 05);
            for (var i = 0; i < 50; i++)
            {
                ApplicationHistory.Add(new Application { timestamp = endDate.AddMinutes(-10) });
            }

            for (var i = 0; i < 2; i++)
            {
                HighRiskEvents.Add(new Application { timestamp = endDate.AddMinutes(-10) });
            }

            _mockThresholdProvider.Setup(_ => _.GetThreshold(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(4);
            _mockClock.Setup(_ => _.Now).Returns(endDate);

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCREASE_HIGH_RISK").Should().BeNull();
        }

        [Test]
        public async Task Returns_HighRiskEventModel_With_CorrectDescription()
        {
            var endDate = new DateTime(2017, 04, 05);
            for (var i = 0; i < 50; i++)
            {
                ApplicationHistory.Add(new Application { timestamp = endDate.AddMinutes(-10) });
            }
            const int applicationsCount = 10;
            for (var i = 0; i < applicationsCount; i++)
            {
                HighRiskEvents.Add(new Application { timestamp = endDate.AddMinutes(-10) });
            }

            _mockThresholdProvider.Setup(_ => _.GetThreshold(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(4);
            _mockClock.Setup(_ => _.Now).Returns(endDate);

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCREASE_HIGH_RISK").event_description.Should().Be("Total application count: 50, high risk application count 10, 20% of 4% threshold");
        }

        [Test]
        public async Task Passes_CorrectDateRange_ToThresholdProvider()
        {
            var startDate = new DateTime(2017, 05, 01);
            var endDate = new DateTime(2017, 05, 02);

            _mockClock.Setup(_ => _.Now).Returns(endDate);
            _mockThresholdProvider.Setup(_ => _.GetThreshold(startDate, endDate)).Returns(4).Verifiable();

            var result = _eventsController.Get();

            _mockThresholdProvider.Verify();
        }

        [Test]
        public async Task Counts_Applications_Within_Window()
        {
            var startDate = new DateTime(2017, 05, 01);
            var endDate = new DateTime(2017, 05, 02);
            _mockClock.Setup(_ => _.Now).Returns(endDate);
            _mockThresholdProvider.Setup(_ => _.GetThreshold(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(4);

            var windowApps = Builder<Application>.CreateListOfSize(100).All().With(_ => _.timestamp = endDate.AddHours(-5)).Build();
            var outWindowApps = Builder<Application>.CreateListOfSize(5).All().With(_ => _.timestamp = endDate.AddDays(-3)).Build();

            foreach (var windowApp in windowApps)
            {
                ApplicationHistory.Add(windowApp);
            }

            foreach (var outWindowApp in outWindowApps)
            {
                ApplicationHistory.Add(outWindowApp);
            }

            var windowHighRiskApps = Builder<Application>.CreateListOfSize(10).All().With(_ => _.timestamp = endDate.AddHours(-5)).Build();
            var outWindowHighRiskApps = Builder<Application>.CreateListOfSize(5).All().With(_ => _.timestamp = endDate.AddDays(-3)).Build();

            foreach (var windowApp in windowHighRiskApps)
            {
                HighRiskEvents.Add(windowApp);
            }

            foreach (var outWindowApp in outWindowHighRiskApps)
            {
                HighRiskEvents.Add(outWindowApp);
            }

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCREASE_HIGH_RISK").event_description.Should().Be("Total application count: 100, high risk application count 10, 10% of 4% threshold");
        }
    }
}
