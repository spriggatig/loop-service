using System;
using System.Collections.Generic;
using System.Web.Http;
using Aire.LoopService.Api.Models;

namespace Aire.LoopService.Api.Controllers
{
    public class EventsController : ApiController
    {
        public IEnumerable<EventModel> Get()
        {
            return new[] { new EventModel { event_name = "INCRESE_HIGH_RISK", event_datetime = DateTime.Now } };
        }
    }
}
