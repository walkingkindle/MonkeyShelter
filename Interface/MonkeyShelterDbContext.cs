using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class MonkeyShelterDbContext : DbContext
    {
        public MonkeyShelterDbContext(DbContextOptions<MonkeyShelterDbContext> options)
            : base(options)  
        {
        }
        public DbSet<Monkey> Monkeys { get; set; } 

        public DbSet<Admission> Admissions { get; set; }

        public DbSet<Departure> Departures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
