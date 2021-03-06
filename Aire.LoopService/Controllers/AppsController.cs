﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Aire.LoopService.Api.Models;
using Aire.LoopService.Domain;
using Aire.LoopService.Events;
using AutoMapper;

namespace Aire.LoopService.Api.Controllers
{
    public class AppsController : ApiController
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly IMapper _mapper;

        public AppsController(IEventProcessor eventProcessor, IMapper mapper)
        {
            _eventProcessor = eventProcessor;
            _mapper = mapper;
        }

        public async Task Post([FromBody]AppModel[] applications)
        {
            ApplicationsCount.Add(applications.Length);
            foreach (var application in applications)
            {
                var mappedApplication = _mapper.Map<Application>(application);
                await _eventProcessor.Process(mappedApplication);
            }
        }
    }
}
