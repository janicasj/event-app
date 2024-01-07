using EventApp.DTOs;
using EventApp.Models;

namespace EventApp.Interfaces
{
    public interface IPersonService
    {
        List<Person> GetPersons(string? name = null, string? tyyppi = null);
    }
}
