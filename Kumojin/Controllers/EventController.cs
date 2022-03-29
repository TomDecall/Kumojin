using ApiModels;
using IService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IEnumerable<EventViewModel>> Get()
        {
            return await _eventService.Get();
        }

        [HttpPost]
        public async Task Post([FromBody] EventViewModel eventViewModel)
        {
            await _eventService.Create(EventViewModelMapper.Map(eventViewModel));
        }
    }
}
