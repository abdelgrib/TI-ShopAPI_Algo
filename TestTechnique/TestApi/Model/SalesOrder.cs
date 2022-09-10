using System;
using System.Collections.Generic;

namespace TestApi.Model
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            OrderItems = new HashSet<SalesOrderItem>();
        }
        
        public int Id { get; set; }
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public string Status { get; set; }
        
        public virtual ICollection<SalesOrderItem> OrderItems { get; set; }
    }
}