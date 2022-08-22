using Microsoft.EntityFrameworkCore;
using Billing.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Billing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly BillDBContext _context;

        public InventoryController(BillDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save inventory
        /// </summary>
        /// <returns>Save inventory</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveInventory(int id, Inventory inventory)
        {
            if (id != inventory.IdInventory)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Check if inventory exists in DB
        /// </summary>
        /// <returns>Bool answer</returns>
        private bool InventoryExists(int id)
        {
            return (_context.Inventories?.Any(x => x.IdInventory == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Post inventory
        /// </summary>
        /// <returns>Inventory data</returns>
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            if (_context.Inventories == null)
            {
                return Problem("'>>>BillContext.Inventories<<<  is NULL.");
            }
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = inventory.IdInventory }, inventory);
        }

        /// <summary>
        /// Delete inventory
        /// </summary>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelInventory(int id)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get inventories
        /// </summary>
        /// <returns>Inventory list</returns>
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            return await _context.Inventories.ToListAsync();
        }

        /// <summary>
        /// Get inventory
        /// </summary>
        /// <returns>Inventory data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }
    }
}
