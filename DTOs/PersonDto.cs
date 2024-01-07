namespace EventApp.DTOs
{
    public class PersonDto
    {
        /// <summary>
        /// Id of the person
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the person
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Birthdate of the person
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Person's events
        /// </summary>
        public List<SimpleEventDto> Events { get; set; } = new List<SimpleEventDto>();
    }
}