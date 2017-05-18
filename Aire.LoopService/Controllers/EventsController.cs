using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Events;

namespace Aire.LoopService.Api.Controllers
{
    public class EventsController : ApiController
    {
        public IEnumerable<EventModel> Get()
        {
            var highriskApplications = HighRiskEvents.Get();

            return new[] { new EventModel { event_name = "INCRESE_HIGH_RISK", count = highriskApplications.Count, event_datetime = DateTime.Now } };
        }
    }
}
