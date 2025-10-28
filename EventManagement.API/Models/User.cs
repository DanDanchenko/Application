namespace EventManagement.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
        public ICollection<Participant> Participations { get; set; } = new List<Participant>();

    }
}
