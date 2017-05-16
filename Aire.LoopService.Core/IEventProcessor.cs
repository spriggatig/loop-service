using System.Threading.Tasks;
using Aire.LoopService.Domain;

namespace Aire.LoopService.Core
{
    public interface IEventProcessor
    {
        Task Process(Application application);
    }
}
