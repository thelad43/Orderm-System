namespace OrdermSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using OrdermSystem.Data.Models;

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
               .HasMany(c => c.Purchases)
               .WithOne(po => po.Customer)
               .HasForeignKey(po => po.CustomerId);
        }
    }
}