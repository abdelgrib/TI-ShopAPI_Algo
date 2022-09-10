namespace TestApi.Model
{
    public class SalesOrderItem
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string TrackingNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }
        
        public SalesOrder SalesOrder { get; set; }
        public Product Product { get; set; }
        
    }
}