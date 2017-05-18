using Aire.LoopService.Api.Models;
using Aire.LoopService.Domain;
using AutoMapper;

namespace Aire.LoopService.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppModel, Application>();
        }
    }
}