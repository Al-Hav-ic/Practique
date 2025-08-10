using Kurs.Classes;

namespace Kurs.Classes
{
    public class Event
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int? OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
        public List<Participant> Participants { get; set; } = new List<Participant>();
        public List<Review> Reviews { get; set; } = new List<Review>();

    }
}
