using System.Collections.Generic;
using System.Web.Http;

namespace Aire.LoopService.Controllers
{
    public class EventsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
