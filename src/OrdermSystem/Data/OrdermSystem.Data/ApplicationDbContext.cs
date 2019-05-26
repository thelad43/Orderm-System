namespace OrdermSystem.Data
{
    using Microsoft.EntityFrameworkCore;

    using OrdermSystem.Data.Configurations;
    using OrdermSystem.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        private readonly Configuration configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
            this.configuration = new Configuration();
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<PurchaseOrder> Purchases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.configuration.Configure<Customer, CustomerConfiguration>(builder);

            base.OnModelCreating(builder);
        }
    }
}