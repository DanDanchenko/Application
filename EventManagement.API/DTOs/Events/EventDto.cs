using EventManagement.API.Models;

namespace EventManagement.API.DTOs.Events
{
    public class EventDto
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
        public int NumberOfParticipants { get; set; }
        public List<string> Participants { get; set; }


    }
}
