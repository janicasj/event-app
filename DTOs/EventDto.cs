namespace EventApp.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Type of the event
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Time of the event
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// Names of the people attending to the event
        /// </summary>
        public List<string?> Persons { get; set; } = new List<string?>();
    }
}