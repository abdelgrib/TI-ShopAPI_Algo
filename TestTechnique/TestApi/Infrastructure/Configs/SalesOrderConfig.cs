using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Model;

namespace TestApi.Infrastructure.Configs
{
    public class SalesOrderConfig : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.Property(e => e.Status).HasMaxLength(50);
            
            builder.HasOne(d => d.Customer)
                .WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_SalesOrders_Customers");
        }
    }
}