using Domain.DatabaseModels;
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
        public DbSet<MonkeyDbModel> Monkeys { get; set; }

        public DbSet<AdmissionDbModel> Admissions { get; set; }

        public DbSet<DepartureDbModel> Departures { get; set; }

        public DbSet<ShelterManagerDbModel> ShelterManagers { get; set; }

        public DbSet<ShelterDbModel> Shelters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                modelBuilder.Entity<ShelterDbModel>()
                    .HasOne(s => s.ShelterManager)
                    .WithOne(sm => sm.Shelter)
                    .HasForeignKey<ShelterDbModel>(s => s.ShelterManagerId);

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
