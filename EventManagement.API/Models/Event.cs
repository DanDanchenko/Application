namespace EventManagement.API.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public int? Capacity { get; set; }
        public bool IsPublic { get; set; } = true;
        public Guid OrganizerId { get; set; }
        public User Organizer { get; set; }
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}
