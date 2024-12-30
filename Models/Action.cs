using System.ComponentModel.DataAnnotations;

namespace E_Wolontariat.Models
{
    public class Action
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public string CreatedBy { get; set; } // Organizacja, która utworzyła akcję
        public ICollection<Application> Applications { get; set; }
    }
}
