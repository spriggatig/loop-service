using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Aire.LoopService.Api.Controllers;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Domain;
using Aire.LoopService.Events;
using AutoMapper;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Aire.LoopService.Api.Tests.ControllerTests
{
    [TestFixture]
    public class EventsControllerTests
    {
        private EventsController _eventsController;

        [SetUp]
        public void SetUp()
        {
            _eventsController = new EventsController();
        }

        [Test]
        public async Task Returns_HighRiskEventModel()
        {
            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCRESE_HIGH_RISK").Should().NotBeNull();
        }

        [Test]
        public async Task Returns_HighRiskEventModel_With_CorrectCount()
        {
            const int applicationsCount = 10;
            HighRiskEvents.Clear();
            for (var i = 0; i < applicationsCount; i++)
            {
                HighRiskEvents.Add(new Application());
            }

            var result = _eventsController.Get();

            result.As<IEnumerable<EventModel>>().FirstOrDefault(_ => _.event_name == "INCRESE_HIGH_RISK").count.Should().Be(applicationsCount);
        }
    }
}
