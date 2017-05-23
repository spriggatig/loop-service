using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aire.LoopService.Api.Controllers;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Domain;
using Aire.LoopService.Events;
using FluentAssertions;
using NUnit.Framework;
using Moq;
using System;

namespace Aire.LoopService.Api.Tests.ControllerTests
{
    [TestFixture]
    public class EventsControllerTests
    {
        private Mock<IClock> _mockClock;
        private EventsController _eventsController;

        [SetUp]
        public void SetUp()
        {
            _mockClock = new Mock<IClock>();
            _eventsController = new EventsController(_mockClock.Object);
        }

        [Test]
        public async Task Returns_HighRiskEvent()
        {
            ApplicationHistory.Clear();
            for (var i = 0; i < 100; i++)
            {
                ApplicationHistory.Add(new Application());
            }

            HighRiskEvents.Clear();
            for (var i = 0; i < 6; i++)
            {
                HighRiskEvents.Add(new Application());
            }

            _mockClock.Setup(_ => _.Now).Returns(new DateTime(2017, 01, 01));

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCREASE_HIGH_RISK").Should().NotBeNull();
        }

        [Test]
        public async Task DoesNot_Return_HighRiskEvent()
        {
            ApplicationHistory.Clear();
            for (var i = 0; i < 50; i++)
            {
                ApplicationHistory.Add(new Application());
            }
            HighRiskEvents.Clear();
            for (var i = 0; i < 2; i++)
            {
                HighRiskEvents.Add(new Application());
            }

            _mockClock.Setup(_ => _.Now).Returns(new DateTime(2017, 01, 01));

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCREASE_HIGH_RISK").Should().BeNull();
        }

        [Test]
        public async Task Returns_HighRiskEventModel_With_CorrectDescription()
        {
            ApplicationHistory.Clear();
            for (var i = 0; i < 50; i++)
            {
                ApplicationHistory.Add(new Application());
            }
            const int applicationsCount = 10;
            HighRiskEvents.Clear();
            for (var i = 0; i < applicationsCount; i++)
            {
                HighRiskEvents.Add(new Application());
            }

            _mockClock.Setup(_ => _.Now).Returns(new DateTime(2017, 01, 01));

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCREASE_HIGH_RISK").event_description.Should().Be("Total application count: 50, high risk application count 10, 20% of 4% threshold");
        }

        [Test]
        public async Task Passes_CorrectDateRange_ToThresholdProvider()
        {
        }
    }
}
