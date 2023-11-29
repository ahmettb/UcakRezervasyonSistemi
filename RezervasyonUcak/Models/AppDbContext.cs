
using Microsoft.EntityFrameworkCore;

namespace RezervasyonUcak.Models




{
    public class AppDbContext:DbContext
    {



        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration=configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }






        public DbSet<Register>Registers { get; set; }
        public DbSet<UcakModel> UcakModel { get; set; }
        public DbSet<Ucak> Ucak { get; set; }
        public DbSet<Firma> Firma { get; set; }
        public DbSet<Koltuk> Koltuk { get; set; }
        public DbSet<Musteri> Musteri { get; set; }



    }
}
