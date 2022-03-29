using Database;
using System.ComponentModel.DataAnnotations;

namespace ApiModels
{
    public class EventViewModel
    {
        public int Id { get; set; } = 0;
        [MaxLength(32)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
    }

    public static class EventViewModelMapper
    {
        public static Event Map(EventViewModel toMap)
        {
            return new Event
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Description = toMap.Description,
                StartDate = toMap.StartDate,
                EndDate = toMap.EndDate,
            };
        }

        public static EventViewModel Map(Event toMap)
        {
            return new EventViewModel
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Description = toMap.Description,
                StartDate = DateTime.SpecifyKind(toMap.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(toMap.EndDate, DateTimeKind.Utc),
            };
        }

        public static List<EventViewModel> Map(List<Event> toMap)
        {
            return toMap.Select(x => Map(x)).ToList();
        }

        public static List<Event> Map(List<EventViewModel> toMap)
        {
            return toMap.Select(x => Map(x)).ToList();
        }
    }
}