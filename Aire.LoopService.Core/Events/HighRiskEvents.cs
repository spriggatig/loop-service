using System;
using System.Collections.Generic;
using Aire.LoopService.Domain;

namespace Aire.LoopService.Events
{
    public static class HighRiskEvents
    {
        private static List<Application> _applications;

        static HighRiskEvents()
        {
            _applications = new List<Application>();
        }

        public static void Add(Application value)
        {
            _applications.Add(value);
        }

        public static void Clear()
        {
            _applications = new List<Application>();
        }

        public static List<Application> Get()
        {
            return _applications;
        }
    }

}
