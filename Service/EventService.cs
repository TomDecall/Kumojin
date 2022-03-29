using ApiModels;
using Database;
using IService;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class EventService : IEventService
    {
        private readonly EventManagementContext _eventManagementContext;

        public EventService(EventManagementContext eventManagementContext)
        {
            _eventManagementContext = eventManagementContext;
        }

        public async Task<int> Create(Event @event)
        {
            await _eventManagementContext.AddAsync(@event);
            return await _eventManagementContext.SaveChangesAsync();
        }

        public async Task<List<EventViewModel>> Get()
        {
            List<Event> events = await _eventManagementContext.Events.ToListAsync();
            return EventViewModelMapper.Map(events);
        }
    }
}