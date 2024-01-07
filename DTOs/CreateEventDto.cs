using EventApp.Models;

namespace EventApp.DTOs
{
    /// <summary>
    /// Dto for creating an event
    /// </summary>
    public class CreateEventDto
    {
        /// <summary>
        /// Type of the event
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Time of the event
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// Id's of the attending people
        /// </summary>
        public List<int> PersonIds { get; set; } = new List<int>();
    }
}