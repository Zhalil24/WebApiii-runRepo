using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiii.Models;

namespace WebApiii.Models
{
    public class ProductsContext : IdentityDbContext<AppUser , AppRole , int>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }

        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Varlık konfigürasyonları buraya eklenir
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=webapiiidb,1433;Database=products; User Id=sa;Password=mAlatya.44a;TrustServerCertificate=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }




    }

}
