using Aire.LoopService.Domain;
using System.Collections.Generic;

namespace Aire.LoopService
{
    public static class ApplicationHistory
    {
        private static List<Application> _applications;

        static ApplicationHistory()
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
