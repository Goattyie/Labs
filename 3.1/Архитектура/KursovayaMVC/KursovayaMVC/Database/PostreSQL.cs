using KursovayaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace KursovayaMVC.Database
{
    public class PostreSQL : DbContext
    {
        public PostreSQL()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=database.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasIndex(a => new { a.Name }).IsUnique(true);
        }

        public DbSet<Service> Services { get; set; }
    }
}
