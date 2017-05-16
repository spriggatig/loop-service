using System.Web.Http;
using Aire.LoopService.Models;

namespace Aire.LoopService.Controllers
{
    public class AppsController : ApiController
    {
        public void Post([FromBody]AppModel[] applications)
        {
        }
    }
}
