using EventApp.DTOs;
using EventApp.Interfaces;
using EventApp.Models;
using System.Data.Entity;
using System.Runtime.InteropServices;

namespace EventApp.Services
{
    public class EventRepository : IEventService
    {
        private readonly EventDbContext _dbContext;
        public EventRepository(EventDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Event CreateEvent(CreateEventDto dto)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var @event = new Event(dto.Type, dto.Time);
                    var persons = _dbContext.Persons.Where(h => dto.PersonIds.Contains(h.PersonId));
                    if (!persons.Any())
                    {
                        throw new ArgumentException("No persons found with given ids.");
                    }
                    foreach (var person in persons)
                    {
                        @event.Persons.Add(person);
                    }
                    _dbContext.Events.Add(@event);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return @event;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException($"Error while creating an event.", ex);
                }
            }
        }

        public void DeleteEvent(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var @event = _dbContext.Events.Where(t => t.EventId == id).FirstOrDefault();
                if (@event is null)
                {
                    transaction.Rollback();
                    throw new NotFoundException($"Event with id {id} not found.");
                }
                try
                {
                    _dbContext.Events.Remove(@event);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException($"Error while deleting an event with id {id}.", ex);
                }
            }
        }

        public List<Event> GetEvents(string? type = null, DateTime? time = null)
        {
            var query = _dbContext.Events.AsQueryable();
            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(t => t.Type == type);
            }
            if (time.HasValue)
            {
                query = query.Where(t => t.Time == time);
            }
            var events = query.Include(e => e.Persons).ToList();
            return events;
        }

    }
}
