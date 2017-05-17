using System.Web.Http;
using Aire.LoopService.Api.Models;

namespace Aire.LoopService.Api.Controllers
{
    public class AppsController : ApiController
    {
        public void Post([FromBody]AppModel[] applications)
        {
        }
    }
}
