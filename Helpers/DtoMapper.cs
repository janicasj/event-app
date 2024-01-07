using EventApp.DTOs;
using EventApp.Models;
using System.Runtime.InteropServices;

namespace EventApp.Helpers
{
    public static class DtoMapper
    {
        /// <summary>
        /// Maps events to eventDtos
        /// </summary>
        /// <param name="events">List of events</param>
        /// <returns>List of eventDtos</returns>
        public static List<EventDto> EventsToDtos(List<Event> events)
        {
            var eventDtos = new List<EventDto>();
            foreach (var @event in CollectionsMarshal.AsSpan<Event>(events))
            {
                eventDtos.Add(new EventDto
                {
                    Id = @event.EventId,
                    Type = @event.Type,
                    Time = @event.Time,
                    Persons = @event.Persons.Select(p => p.Name).ToList()
                });
            }
            return eventDtos;
        }

        /// <summary>
        /// Maps persons to personDtos
        /// </summary>
        /// <param name="persons">List of persons</param>
        /// <returns>List of personDtos</returns>
        public static List<PersonDto> PersonsToDtos(List<Person> persons)
        {
            var personDtos = new List<PersonDto>();

            foreach (var person in CollectionsMarshal.AsSpan<Person>(persons))
            {
                var personDto = new PersonDto
                {
                    Id = person.PersonId,
                    Name = person.Name,
                    BirthDate = person.Birthdate,
                    Events = person.Events.Select(e =>
                        new SimpleEventDto
                        {
                            Id = e.EventId,
                            Time = e.Time,
                            Type = e.Type
                        }).ToList()
                };

                personDtos.Add(personDto);
            }

            return personDtos;
        }
    }
}
