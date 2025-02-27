using Microsoft.EntityFrameworkCore;
using WEBAPPP.Models; // Vérifie bien le namespace

namespace WEBAPPP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Déclaratuion les tables ici (apres conseption)
        // Table temporaire pour tester la base de données
        public DbSet<Test> Tests { get; set; }
    }
}

