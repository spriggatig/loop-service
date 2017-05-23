using System;
using System.Collections.Generic;
using System.Web.Http;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Events;

namespace Aire.LoopService.Api.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IClock _clock;

        public EventsController(IClock clock)
        {
            _clock = clock;
        }

        public IEnumerable<EventModel> Get()
        {
            var events = new List<EventModel>();
            var startDate = _clock.Now.AddDays(-1);

            var applications = ApplicationHistory.Get();
            var applicationsCount = applications.Count;
            var highriskApplications = HighRiskEvents.Get();
            var highriskApplicationsCount = highriskApplications.Count;
            var threshold = 4; // 4% threshold, todo: make this configurable
            if (applicationsCount > 0 && highriskApplicationsCount > 0)
            {
                var percentHighRisk = (int)Math.Round((double)(100 * highriskApplicationsCount) / applicationsCount);

                if (percentHighRisk > threshold)
                {
                    var eventDescription = $"Total application count: {applicationsCount}, high risk application count {highriskApplications.Count}, {percentHighRisk}% of {threshold}% threshold";
                    events.Add(new EventModel { event_name = "INCREASE_HIGH_RISK", event_description = eventDescription, event_datetime = DateTime.Now });
                }
            }

            return events.ToArray();
        }
    }
}
