using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billing.Api.Models;
using Billing.Api.DTOs;

namespace Billing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BillDBContext _context;

        public BillController(BillDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save a bill 
        /// </summary>
        /// <returns>Save bill</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveBill(int id, Bill bill)
        {
            if (id != bill.IdBill)
            {
                return BadRequest();
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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
        /// Check if a bill exists in DB
        /// </summary>
        /// <returns>Nothing</returns>
        private bool BillExists(int id)
        {
            return (_context.Bills?.Any(x => x.IdBill == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Delete a bill
        /// </summary>
        /// <returns>Nothing</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelBill(int id)
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get all bills 
        /// </summary>
        /// <returns>Bills list</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            List<Bill> bills = await _context.Bills.Include(x => x.IdCustomerNavigation).Include(x => x.BillDetails).ToListAsync();
            bills.Select(x => new Bill
            {
                IdCustomerNavigation = new Customer
                {
                    IdCustomer = x.IdCustomer,
                    FirstName = x.IdCustomerNavigation.FirstName,
                    LastName = x.IdCustomerNavigation.LastName,

                },
                IdCustomer = x.IdCustomer,
                IdBill = x.IdBill,
                BillDate = x.BillDate,
                BillDetails = x.BillDetails.ToList().Select(y => new BillDetail
                {
                    IdBillDetail = y.IdBillDetail,
                    Description = y.Description,
                }).ToList(),
            });

            return Ok(bills);
        }

        /// <summary>
        /// Get bill 
        /// </summary>
        /// <returns>Bills data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BillDetailInvDTO>> GetBill(int id)
        {
          if (_context.Bills == null)
          {
              return NotFound();
          }

            var bill = await _context.Bills
                .Include(b => b.BillDetails)
                .ThenInclude(d => d.IdProductNavigation)
                .Where(d => d.IdBill == id)
                .FirstAsync();

            List<BillDetailInvDTO> details = new List<BillDetailInvDTO>();

            bill.BillDetails.ToList().ForEach(d =>
            {
                BillDetailInvDTO detail = new BillDetailInvDTO();
                detail.ProductName = d.IdProductNavigation.ProductName;
                detail.Price = d.IdProductNavigation.Price;
                detail.Quantity = d.Quantity;
                details.Add(detail);
            });

            if (bill == null)
            {
                return NotFound();
            }

            return Ok(details);
        }

        /// <summary>
        /// Get bill 
        /// </summary>
        /// <returns>Bill data</returns>
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(BillDTO billDTO)
        {
            if (_context.Bills == null)
            {
                return Problem("'>>>BillContext.Bills<<<  is NULL.");
            }

            Bill bill = new Bill();
            bill.BillDate = billDTO.BillDate;
            bill.IdCustomer = billDTO.IdCustomer;

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.IdBill }, bill);
        }


    }
}
