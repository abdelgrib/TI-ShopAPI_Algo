using System.Collections.Generic;

namespace TestApi.Model
{
    public class Customer
    {
        public Customer()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }
        
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}