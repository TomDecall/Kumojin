using ApiModels;
using Database;

namespace IService
{
    public interface IEventService
    {
        public Task<List<EventViewModel>> Get();
        public Task<int> Create(Event @event);
    }
}