using System.Linq;
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
        private readonly IThresholdProvider _thresholderProvider;

        public EventsController(IClock clock, IThresholdProvider thresholdProvider)
        {
            _clock = clock;
            _thresholderProvider = thresholdProvider;
        }

        public IEnumerable<EventModel> Get()
        {
            var events = new List<EventModel>();
            var startDate = _clock.Now.AddDays(-1);
            var endDate = _clock.Now;

            var threshold = _thresholderProvider.GetThreshold(startDate, endDate);

            var applications = ApplicationHistory.Get();
            var highriskApplications = HighRiskEvents.Get();

            var applicationsInWindow = applications.Where(_ => _.timestamp > startDate && _.timestamp < endDate);
            var highRiskApplicationsInWindow = highriskApplications.Where(_ => _.timestamp > startDate && _.timestamp < endDate);

            var applicationsInWindowCount = applicationsInWindow.Count();
            var highRiskApplicationsInWindowCount = highRiskApplicationsInWindow.Count();

            if (applicationsInWindowCount > 0 && highRiskApplicationsInWindowCount > 0)
            {
                var percentHighRisk = (int)Math.Round((double)(100 * highRiskApplicationsInWindowCount) / applicationsInWindowCount);

                if (percentHighRisk > threshold)
                {
                    var eventDescription = $"Total application count: {applicationsInWindowCount}, high risk application count {highRiskApplicationsInWindowCount}, {percentHighRisk}% of {threshold}% threshold";
                    events.Add(new EventModel { event_name = "INCREASE_HIGH_RISK", event_description = eventDescription, event_datetime = endDate });
                }
            }

            return events.ToArray();
        }
    }
}
