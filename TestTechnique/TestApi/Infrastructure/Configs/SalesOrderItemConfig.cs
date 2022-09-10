using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Model;

namespace TestApi.Infrastructure.Configs
{
    public class SalesOrderItemConfig : IEntityTypeConfiguration<SalesOrderItem>
    {
        public void Configure(EntityTypeBuilder<SalesOrderItem> builder)
        {
            builder.Property(e => e.TrackingNumber).HasMaxLength(200);
            
            builder.HasOne(d => d.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_SalesOrderItemss_Products");
            
            builder.HasOne(d => d.SalesOrder)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.SalesOrderId)
                .HasConstraintName("FK_SalesOrderItemss_Orders");
        }
    }
}