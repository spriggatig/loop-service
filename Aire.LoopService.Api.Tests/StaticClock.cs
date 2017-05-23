using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aire.LoopService.Api.Tests
{
    public class StaticClock : IClock
    {
        public DateTime Now { get { return new DateTime(2008, 09, 3, 9, 6, 13); } }
    }
}
