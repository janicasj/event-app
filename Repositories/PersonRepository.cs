using EventApp.DTOs;
using EventApp.Interfaces;
using EventApp.Models;
using System.Data.Entity;

namespace EventApp.Services
{
    public class PersonRepository : IPersonService
    {
        readonly EventDbContext _dbContext;
        public PersonRepository(EventDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public List<Person> GetPersons(string? name = null, string? eventType = null)
        {
            var query = _dbContext.Persons.AsQueryable();
            if (name != null)
            {
                query = query.Where(p => name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
            }
            if (eventType != null)
            {
                query = query.Where(p => p.Events.Any(e => e.Type == eventType));
            }
            var persons = query.Include(p => p.Events).ToList();
            return persons;
        }

       
    }
}
