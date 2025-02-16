using Microsoft.AspNetCore.Mvc;
using Elysian.Data; // Replace with your actual namespace
using Elysian.Models; // Replace with your actual namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Elysian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Store
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create([FromBody] Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors as JSON
            }

            store.CreatedAt = DateTime.UtcNow;
            Console.WriteLine(store);

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = store.Id }, store); // Return 201 with the created resource
        }

        // GET: api/Store/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound(); // Return 404 if not found
            }

            return Ok(store); // Return 200 with the store object
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetAll()
        {
            var stores = await _context.Stores.ToListAsync();
            return Ok(stores); // Return 200 with the list of stores
        }

        // PUT: api/Store/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Store updatedStore)
        {
            // Print the values to the console
            Console.WriteLine($"Received id: {id}");
            Console.WriteLine($"Received updatedStore.Id: {updatedStore.Id}");
            if (id != updatedStore.Id)
            {
                return BadRequest("Store ID mismatch."); // Return 400 for ID mismatch
            }

            var existingStore = await _context.Stores.FindAsync(id);
            if (existingStore == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Update fields
            existingStore.Name = updatedStore.Name;
            existingStore.Description = updatedStore.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the store.");
            }

            return NoContent(); // Return 204 No Content
        }

        // DELETE: api/Store/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound(); // Return 404 if not found
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content
        }
    }
}
