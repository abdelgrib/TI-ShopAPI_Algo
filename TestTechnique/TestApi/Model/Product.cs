using System.Collections.Generic;

namespace TestApi.Model
{
    public class Product
    {
        public Product()
        {
            OrderItems = new HashSet<SalesOrderItem>();
        }
        
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<SalesOrderItem> OrderItems { get; set; }
    }
}