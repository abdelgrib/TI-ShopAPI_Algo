using Microsoft.EntityFrameworkCore;
using TestApi.Infrastructure.Configs;
using TestApi.Model;

namespace TestApi
{
    public class TestApiDbContext : DbContext
    {
        #region Constructors

        public TestApiDbContext()
        {
        }

        public TestApiDbContext(DbContextOptions<TestApiDbContext> options)
            : base(options)
        {
        }

        #endregion
        
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new SalesOrderConfig());
            modelBuilder.ApplyConfiguration(new SalesOrderItemConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
        }
    }
}