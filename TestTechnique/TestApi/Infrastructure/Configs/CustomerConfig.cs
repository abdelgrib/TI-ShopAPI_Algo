using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Model;

namespace TestApi.Infrastructure.Configs
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.Firstname).HasMaxLength(200);
            builder.Property(e => e.Email).HasMaxLength(200).IsRequired();
            builder.Property(e => e.Lastname).HasMaxLength(200);
            builder.Property(e => e.AccountNumber).HasMaxLength(200);
        }
    }
}