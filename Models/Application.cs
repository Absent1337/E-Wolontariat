using Microsoft.AspNetCore.Identity;

namespace E_Wolontariat.Models
{
    public class Application
    {
        public int Id { get; set; }

        public string UserId { get; set; } // Klucz obcy do tabeli IdentityUser
        public int ActionId { get; set; } // Klucz obcy do tabeli Action
        public string Status { get; set; } // Pending, Accepted, Rejected

        public IdentityUser User { get; set; }
        public Action Action { get; set; }
    }
}
