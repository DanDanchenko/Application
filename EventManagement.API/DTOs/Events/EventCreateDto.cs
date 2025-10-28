namespace EventManagement.API.DTOs.Events
{
    public class EventCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public int? Capacity { get; set; }
        public bool IsPublic { get; set; } = true;
        public string OrganizerEmail { get; set; }

    }
}
