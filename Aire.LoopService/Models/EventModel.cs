using System;

namespace Aire.LoopService.Api.Models
{
    public class EventModel
    {
        public string event_name { get; set; }

        public string event_description { get; set; }

        public DateTime event_datetime { get; set; }
    }
}