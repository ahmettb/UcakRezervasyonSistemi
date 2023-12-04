
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace RezervasyonUcak.Models


{
    public class AppDbContext :DbContext
    {


        public AppDbContext() 
        {

        }

        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration=configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UcusSefer>()
                .HasMany(u => u.Biletler)
                .WithOne(b => b.UcusSefer)
                .HasForeignKey(b => b.UcusSeferId);
        }

        public Task CreateAsync(Musteri user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Musteri user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Musteri user)
        {
            throw new NotImplementedException();
        }

        public Task<Musteri> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Musteri> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public DbSet<UcakModel> UcakModel { get; set; }
        public DbSet<Ucak> Ucak { get; set; }
        public DbSet<Bilet> Bilets { get; set; }


        public DbSet<Firma> Firma { get; set; }
        public DbSet<Koltuk> Koltuk { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<UcusSefer> UcusSefers { get; set; }




    }
}
