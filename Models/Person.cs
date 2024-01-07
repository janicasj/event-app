namespace EventApp.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string? Name { get; set; }

    public DateTime? Birthdate { get; set; }

    public virtual ICollection<Event> Events { get; set; }

    public Person(string? name, DateTime? birthdate)
    {
        Name = name;
        Birthdate = birthdate;
        Events = new List<Event>();
    }
}
