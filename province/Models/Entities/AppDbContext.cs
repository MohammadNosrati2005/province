using Microsoft.EntityFrameworkCore;

namespace province.Models.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<Province> Provinces { get; set; } =null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(250);
                entity.HasMany(p => p.Cities)
                .WithOne(c => c.province)
                .HasForeignKey(c => c.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(250);
            });
            
        }
    }
}
