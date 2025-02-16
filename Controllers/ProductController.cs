using Microsoft.AspNetCore.Mvc;
using Elysian.Data; // Replace with your actual namespace
using Elysian.Models; // Replace with your actual namespace
using Microsoft.EntityFrameworkCore;

namespace Elysian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            var storeExists = await _context.Stores.FindAsync(product.StoreID);
            if (storeExists == null)
            {
                return NotFound($"Store with ID {product.StoreID} does not exist.");
            }

            product.CreatedDate = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, product);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Store) // Include store details
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Store) // Include store details
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/Product/Store/{storeId}
        [HttpGet("Store/{storeId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByStore(int storeId)
        {
            var storeExists = await _context.Stores.FindAsync(storeId);
            if (storeExists == null)
            {
                return NotFound($"Store with ID {storeId} does not exist.");
            }

            var products = await _context.Products
                .Where(p => p.StoreID == storeId)
                .ToListAsync();

            return Ok(products);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.ProductID)
            {
                return BadRequest("Product ID mismatch.");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            existingProduct.ProductName = updatedProduct.ProductName;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Stock = updatedProduct.Stock;
            existingProduct.StoreID = updatedProduct.StoreID;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }

            return Ok(existingProduct);
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
