using Microsoft.EntityFrameworkCore;
using WEBAPP.Models;
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
        public DbSet<Smartphone> Smartphones { get; set; }
        public DbSet<Smartwatch> Smartwatches { get; set; }
        public DbSet<SmartwatchPrediction> SmartwatchPredictions { get; set; }
        public DbSet<CGM> CGMs { get; set; }
        public DbSet<VehiculeOBU> VehiculeOBUs { get; set; }

    }
}

