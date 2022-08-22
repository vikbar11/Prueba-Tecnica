using Microsoft.EntityFrameworkCore;
using Billing.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BillDBContext _context;

        public CustomerController(BillDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save a customer 
        /// </summary>
        /// <returns>Save customer</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveCustomer(int id, Customer customer)
        {
            if (id != customer.IdCustomer)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExist(id))
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
        /// Check if a client exists in DB
        /// </summary>
        /// <returns>Bool Answer</returns>
        private bool CustomerExist(int id)
        {
            return (_context.Customers?.Any(e => e.IdCustomer == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Get all costumers
        /// </summary>
        /// <returns>Costumers list</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Get a customer 
        /// </summary>
        /// <returns>Customer data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        /// <summary>
        /// Post a customer 
        /// </summary>
        /// <returns>Customer data</returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (_context.Customers == null)
            {
                return Problem("'>>>BillDBContext.Customers<<<  is NULL.");
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.IdCustomer }, customer);
        }

        /// <summary>
        /// Delete a customer 
        /// </summary>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelCostumer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
