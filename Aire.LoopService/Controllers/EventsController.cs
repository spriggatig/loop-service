using System;
using System.Collections.Generic;
using System.Web.Http;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Events;

namespace Aire.LoopService.Api.Controllers
{
    public class EventsController : ApiController
    {
        public IEnumerable<EventModel> Get()
        {
            var events = new List<EventModel>();

            var totalApplicationsCount = ApplicationsCount.Get();
            var highriskApplications = HighRiskEvents.Get();
            var threshold = 4; // 4% threshold, todo: make this configurable
            var percentHighRisk = (int)Math.Round((double)(100 * highriskApplications.Count) / totalApplicationsCount);

            if (percentHighRisk > threshold)
            {
                events.Add(new EventModel { event_name = "INCRESE_HIGH_RISK", count = highriskApplications.Count, event_datetime = DateTime.Now });
            }

            return events.ToArray();
        }
    }
}
