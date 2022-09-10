using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly TestApiDbContext _context;

        public CustomerController(TestApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var result = await _context.Customers.SingleOrDefaultAsync(x => x.Id == customerId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
       
    }
}