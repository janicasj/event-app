using EventApp.DTOs;
using EventApp.Helpers;
using EventApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;
        public PersonController(IPersonService personService) 
        {
            _personService = personService;
        }

        /// <summary>
        /// Gets people and their events.
        /// Can be filtered by the name of the person or type of the event.
        /// </summary>
        /// <param name="name">Name of a person</param>
        /// <param name="type">Type of an event</param>
        /// <response code="200">Returns requested people.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">An internal error has occurred.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPersons([FromQuery] string? name, [FromQuery] string? type)
        {
            var persons = _personService.GetPersons(name, type);
            var personDtos = DtoMapper.PersonsToDtos(persons);
            return Ok(personDtos);
        }
    }
}
