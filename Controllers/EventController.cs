using EventApp.DTOs;
using EventApp.Helpers;
using EventApp.Interfaces;
using EventApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Gets all events.
        /// Can be filtered by type or time of the event.
        /// </summary>
        /// <param name="eventType">Type of an event</param>
        /// <param name="time">Time of an event</param>
        /// <returns>Events and attendees by their names.</returns>
        /// <response code="200">Returns requested events.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">An internal error has occurred.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetEvents([FromQuery] string? eventType, DateTime? time)
        {
            var events = _eventService.GetEvents(eventType, time);
            return Ok(DtoMapper.EventsToDtos(events));
        }

        /// <summary>
        /// Creates an event.
        /// </summary>
        /// <param name="createEventDto">Dto including time and type of event, and attendees' ids.</param>
        /// <returns>Created event</returns>
        /// <response code="201">Event created succesfully.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">An internal error has occurred.</response>
        [HttpPost]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            if (createEventDto == null || createEventDto.PersonIds.IsNullOrEmpty()) 
            {
                return BadRequest();
            }
            var createdEvent = _eventService.CreateEvent(createEventDto);
            return new ObjectResult(DtoMapper.EventsToDtos(new List<Event>() { createdEvent }).First())
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        /// <summary>
        /// Deletes an event.
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <response code="204">Event deleted correctly.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Event not found with the given id.</response>
        /// <response code="500">An internal error has occurred.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEvent([FromQuery][Required] int id)
        {
            try
            {
                _eventService.DeleteEvent(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
