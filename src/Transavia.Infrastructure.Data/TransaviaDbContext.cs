using Microsoft.EntityFrameworkCore;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.Infrastructure.Data
{
    public class TransaviaDbContext : DbContext
    {
        public TransaviaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AirportEntity> Airports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirportEntity>().ToTable("Airports").HasKey(p => p.Id);
            modelBuilder.Entity<AirportEntity>().HasAlternateKey(a => a.Iata);
            modelBuilder.Entity<AirportEntity>().HasAlternateKey(a => a.Name);
            modelBuilder.Entity<AirportEntity>().Property(x => x.Lat).IsRequired();
            modelBuilder.Entity<AirportEntity>().Property(x => x.Lon).IsRequired();
            modelBuilder.Entity<AirportEntity>().Property(x => x.Size).IsRequired(false);
            modelBuilder.Entity<AirportEntity>().HasOne(x => x.Country);
            modelBuilder.Entity<AirportEntity>().HasOne(x => x.Status);
            modelBuilder.Entity<AirportEntity>().HasOne(x => x.Type);

            modelBuilder.Entity<CountryEntity>().ToTable("Countries").HasKey(p => p.Id);
            modelBuilder.Entity<CountryEntity>().HasAlternateKey(a => a.Iso);
            modelBuilder.Entity<CountryEntity>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<CountryEntity>().HasOne(a => a.Continent).WithMany(x => x.Countries);

            modelBuilder.Entity<ContinentEntity>().ToTable("Continents").HasKey(p => p.Id);
            modelBuilder.Entity<ContinentEntity>().HasAlternateKey(a => a.Code);

            modelBuilder.Entity<AirportTypeEntity>().ToTable("AirportTypes").HasKey(p => p.Id);
            modelBuilder.Entity<AirportTypeEntity>().HasAlternateKey(a => a.TypeName);

            modelBuilder.Entity<SizeEntity>().ToTable("Sizes").HasKey(p => p.Id);
            modelBuilder.Entity<SizeEntity>().HasAlternateKey(a => a.SizeName);

            modelBuilder.Entity<StatusEntity>().ToTable("Statuses").HasKey(p => p.Id);
            modelBuilder.Entity<StatusEntity>().HasAlternateKey(x => x.Code);
            modelBuilder.Entity<StatusEntity>().Property(x => x.Name).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}