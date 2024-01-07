namespace EventApp.DTOs
{
    public class SimpleEventDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public DateTime? Time { get; set; }
    }
}