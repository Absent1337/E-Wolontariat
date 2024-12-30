using Microsoft.AspNetCore.Identity;

namespace E_Wolontariat.Models
{
    public class VolunteerAction
    {
        public int Id { get; set; }

        public string VolunteerId { get; set; } // ID wolontariusza (IdentityUser)
        public int ActionId { get; set; } // ID akcji

        public IdentityUser Volunteer { get; set; }
        public Action Action { get; set; }
    }
}
