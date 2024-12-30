using Microsoft.EntityFrameworkCore;
using E_Wolontariat.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_Wolontariat.Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<Application> Applications { get; set; } // Nowa tabela zgłoszeń
        public DbSet<VolunteerAction> VolunteerActions { get; set; }

    }
}
