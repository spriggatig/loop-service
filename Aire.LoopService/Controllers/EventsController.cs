using System;
using System.Collections.Generic;
using System.Web.Http;
using Aire.LoopService.Models;

namespace Aire.LoopService.Controllers
{
    public class EventsController : ApiController
    {
        public IEnumerable<EventModel> Get()
        {
            return new[] { new EventModel { event_name = "INCRESE_HIGH_RISK", event_datetime = DateTime.Now } };
        }
    }
}
