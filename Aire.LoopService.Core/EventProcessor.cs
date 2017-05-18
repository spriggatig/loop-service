using System.Threading.Tasks;
using Aire.LoopService.Domain;
using Aire.LoopService.EventProcessors;

namespace Aire.LoopService
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IIncreaseHighRisk _increaseHighRisk;

        public EventProcessor(IIncreaseHighRisk increaseHighRisk)
        {
            _increaseHighRisk = increaseHighRisk;
        }

        public async Task Process(Application application)
        {
            await _increaseHighRisk.Process(application);
        }
    }
}