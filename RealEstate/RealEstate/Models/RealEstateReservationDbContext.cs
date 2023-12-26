using Microsoft.EntityFrameworkCore;

namespace RealEstate.Models
{
    public class RealEstateReservationDbContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Imgs> Imgs { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<RealEstateReservations> RealEstateReservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Support> Supports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.;Database=RealEstateReservation;Trusted_Connection=True;Encrypt=False");
        }
    }
}