using Microsoft.AspNetCore.Mvc;
using Elysian.Data; // Replace with your actual namespace
using Elysian.Models; // Replace with your actual namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Elysian.Controllers
{
    [Route("api/{storeId}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BillboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BillboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/{storeId}/Billboard
        [HttpPost("{id}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Guid id, [FromBody] Billboard billboard)
        {
            Console.WriteLine($"Received storeid id: {id}");
            Console.WriteLine($"Received billboard store ID: {billboard.StoreId}");
            // Ensure the Store exists
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound("Store not found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            billboard.StoreId = id;
            billboard.CreatedAt = DateTime.UtcNow;
            billboard.UpdatedAt = DateTime.UtcNow;

            _context.Billboards.Add(billboard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { storeId = id, id = billboard.Id }, billboard); // Return 201 with the created resource
        }

        // GET: api/{storeId}/Billboard/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid storeId, Guid id)
        {
            var billboard = await _context.Billboards
                .FirstOrDefaultAsync(b => b.Id == id && b.StoreId == storeId);

            if (billboard == null)
            {
                return NotFound(); // Return 404 if not found
            }

            return Ok(billboard); // Return 200 with the billboard object
        }

        // GET: api/{storeId}/Billboard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Billboard>>> GetAll(Guid storeId)
        {
            var billboards = await _context.Billboards
                .Where(b => b.StoreId == storeId)
                .ToListAsync();

            return Ok(billboards); // Return 200 with the list of billboards
        }

        // PUT: api/{storeId}/Billboard/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid storeId, Guid id, [FromBody] Billboard updatedBillboard)
        {
            if (id != updatedBillboard.Id)
            {
                return BadRequest("Billboard ID mismatch."); // Return 400 for ID mismatch
            }

            var existingBillboard = await _context.Billboards
                .FirstOrDefaultAsync(b => b.Id == id && b.StoreId == storeId);

            if (existingBillboard == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Update fields
            existingBillboard.Label = updatedBillboard.Label;
            existingBillboard.ImageUrl = updatedBillboard.ImageUrl;
            existingBillboard.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the billboard.");
            }

            return NoContent(); // Return 204 No Content
        }

        // DELETE: api/{storeId}/Billboard/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid storeId, Guid id)
        {
            var billboard = await _context.Billboards
                .FirstOrDefaultAsync(b => b.Id == id && b.StoreId == storeId);

            if (billboard == null)
            {
                return NotFound(); // Return 404 if not found
            }

            _context.Billboards.Remove(billboard);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content
        }
    }
}