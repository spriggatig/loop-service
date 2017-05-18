using System.Threading.Tasks;
using Aire.LoopService.Domain;

namespace Aire.LoopService.EventProcessors
{
    public interface IIncreaseHighRisk
    {
        Task Process(Application application);
    }
}