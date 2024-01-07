namespace EventApp.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string? Type { get; set; }

    public DateTime? Time { get; set; }

    public virtual ICollection<Person> Persons { get; set; } = new List<Person>();

    public Event(string type,  DateTime? time)
    {
        Type = type;
        Time = time;
    }
}
