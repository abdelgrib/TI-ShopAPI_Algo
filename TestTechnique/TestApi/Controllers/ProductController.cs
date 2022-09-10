using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly TestApiDbContext _context;

        public ProductController(TestApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{productId}/customers")]
        public async Task<IActionResult> GetProductCustomers(int productId)
        {
            var query1 = _context.Products.Where(x => x.Id == productId)
                .SelectMany(x => x.OrderItems.Select(y => y.SalesOrder.Customer))
                .Distinct();

            var query2 = _context.Customers.Where(
                    x => x.SalesOrders != null
                    && x.SalesOrders.SelectMany(y => y.OrderItems)
                    .Any(y => y.ProductId == productId))
                .Distinct();

            /* Output for 'query1' */
            var queryAsString = query1.ToQueryString();
            //DECLARE @__productId_0 int = 1;
            //SELECT
            //    DISTINCT[t].[Id], [t].[AccountNumber], [t].[Email], [t].[Firstname], [t].[Lastname]
            //FROM[Products] AS[p] INNER JOIN(
            //  SELECT
            //      [c].[Id], [c].[AccountNumber], [c].[Email], [c].[Firstname], [c].[Lastname], [s].[ProductId]
            //  FROM [SalesOrderItems] AS [s] INNER JOIN [SalesOrders] AS[s0] ON [s].[SalesOrderId] = [s0].[Id]
            //  INNER JOIN [Customers] AS[c] ON [s0].[CustomerId] = [c].[Id]
            //) AS[t] ON[p].[Id] = [t].[ProductId]
            //WHERE[p].[Id] = @__productId_0

            /* Output for 'query2' */
            queryAsString = query1.ToQueryString();
            //DECLARE @__productId_0 int = 1;
            //SELECT
            //    DISTINCT [c].[Id], [c].[AccountNumber], [c].[Email], [c].[Firstname], [c].[Lastname] 
            //FROM[Customers] AS[c]
            //WHERE EXISTS(
            //    SELECT 1 FROM[SalesOrders] AS[s]
            //    INNER JOIN[SalesOrderItems] AS[s0] ON[s].[Id] = [s0].[SalesOrderId]
            //    WHERE([c].[Id] = [s].[CustomerId]) AND ([s0].[ProductId] = @__productId_0)
            //)

            var result = await query1.ToListAsync();

            var q1 = @"select 
	                        distinct cu.Id as CustomerId, cu.Firstname --, so.Id as SalesOrderId  
                        from Customers cu
                        left join SalesOrders so on so.CustomerId = cu.Id
                        left join SalesOrderItems soi on soi.SalesOrderId = so.Id --inner
                        left join Products pr on pr.Id = soi.ProductId
                        where pr.Id = 1";

            var q2 = @"select 
	                        cu.Id as CustomerId, cu.Firstname, so.Id as SalesOrderId  
                        from Customers cu
                        left join SalesOrders so on so.CustomerId = cu.Id
                        where cu.Id = 1"; 

            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}