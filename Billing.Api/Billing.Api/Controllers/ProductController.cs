using Microsoft.EntityFrameworkCore;
using Billing.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billing.Api.DTOs;

namespace Billing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly BillDBContext _context;

        public ProductController(BillDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save product
        /// </summary>
        /// <returns>Save product</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveProduct(int id, Product product)
        {
            if (id != product.IdProduct)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        /// Check if a product exists in DB
        /// </summary>
        /// <returns>Bool answer</returns>
        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Post product 
        /// </summary>
        /// <returns>Product</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("'>>>BillContext.Products<<<  is NULL.");
            }            

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.IdProduct }, product);
        }

        /// <summary>
        /// Delete product 
        /// </summary>
        /// <returns>Delete product</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get products
        /// </summary>
        /// <returns>Products data</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Get product
        /// </summary>
        /// <returns>Product data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

    }
}

