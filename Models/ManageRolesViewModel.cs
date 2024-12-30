using Microsoft.AspNetCore.Identity;

namespace E_Wolontariat.Models
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<IdentityRole> AvailableRoles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
