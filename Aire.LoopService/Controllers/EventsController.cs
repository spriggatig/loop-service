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
            var highriskApplicationsCount = highriskApplications.Count;
            var threshold = 4; // 4% threshold, todo: make this configurable
            if (totalApplicationsCount > 0 && highriskApplicationsCount > 0)
            {
                var percentHighRisk = (int)Math.Round((double)(100 * highriskApplicationsCount) / totalApplicationsCount);

                if (percentHighRisk > threshold)
                {
                    var eventDescription = $"Total application count: {totalApplicationsCount}, high risk application count {highriskApplications.Count}, {percentHighRisk}% of {threshold}% threshold";
                    events.Add(new EventModel { event_name = "INCRESE_HIGH_RISK", event_description = eventDescription, event_datetime = DateTime.Now });
                }
            }

            return events.ToArray();
        }
    }
}
