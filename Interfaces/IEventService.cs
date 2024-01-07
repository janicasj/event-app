using EventApp.DTOs;
using EventApp.Models;

namespace EventApp.Interfaces
{
    public interface IEventService
    {
        List<Event> GetEvents(string? tyyppi = null, DateTime? aika = null);
        Event CreateEvent(CreateEventDto dto);
        void DeleteEvent(int id);
    }
}
