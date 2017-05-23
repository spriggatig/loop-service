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

            var previousDayEvent = CheckEvent(startDate, endDate, "INCREASE_HIGH_RISK");
            if (previousDayEvent != null)
            {
                events.Add(previousDayEvent);
            }

            startDate = _clock.Now.AddHours(-1);
            var previousHourEvent = CheckEvent(startDate, endDate, "INCREASE_HIGH_RISK_1HR");
            if (previousHourEvent != null)
            {
                events.Add(previousHourEvent);
            }

            return events.ToArray();
        }

        private EventModel CheckEvent(DateTime startDate, DateTime endDate, string eventName)
        {
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
                    return new EventModel { event_name = eventName, event_description = eventDescription, event_datetime = endDate };
                }
            }

            return null;
        }
    }
}
