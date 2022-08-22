using Microsoft.EntityFrameworkCore;
using Billing.Api.DTOs;
using Billing.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Billing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailController : ControllerBase
    {
        private readonly BillDBContext _context;

        public BillDetailController(BillDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save bill detail
        /// </summary>
        /// <returns>Save bill detail</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveBillDetail(int id, BillDetail billDetail)
        {
            if (id != billDetail.IdBillDetail)
            {
                return BadRequest();
            }

            _context.Entry(billDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillDetailExists(id))
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
        /// Check if a bill detail exists in DB
        /// </summary>
        /// <returns>Bool answer</returns>
        private bool BillDetailExists(int id)
        {
            return (_context.BillDetails?.Any(x => x.IdBillDetail == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Post bill detail 
        /// </summary>
        /// <returns>Bill detail</returns>
        [HttpPost]
        public async Task<ActionResult<BillDetail>> PostBillDetail(BillDetailDTO billDetailDTO)
        {
            if (_context.BillDetails == null)
            {
                return Problem("'>>>BillContext.BillDetails<<<  is NULL.");
            }

            BillDetail billDetail = new BillDetail();
            billDetail.IdBillDetail = billDetailDTO.IdBillDetail;
            billDetail.IdBill = billDetailDTO.IdBill;
            billDetail.Description = billDetail.Description;
            billDetail.IdProduct = billDetailDTO.IdProduct;
            billDetail.Quantity = billDetailDTO.Quantity;

            _context.BillDetails.Add(billDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillDetail", new { id = billDetail.IdBillDetail }, billDetail);
        }

        /// <summary>
        /// Delete bill detail 
        /// </summary>
        /// <returns>Delete bill detail</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelBillDetail(int id)
        {
            if (_context.BillDetails == null)
            {
                return NotFound();
            }
            var billDetail = await _context.BillDetails.FindAsync(id);
            if (billDetail == null)
            {
                return NotFound();
            }

            _context.BillDetails.Remove(billDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get bill details 
        /// </summary>
        /// <returns>Bill details data</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillDetail>>> GetBillDetails()
        {
            if (_context.BillDetails == null)
            {
                return NotFound();
            }
            return await _context.BillDetails.ToListAsync();
        }

        /// <summary>
        /// Get bill detail 
        /// </summary>
        /// <returns>Bill detail data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BillDetail>> GetBillDetail(int id)
        {
            if (_context.BillDetails == null)
            {
                return NotFound();
            }
            var billDetail = await _context.BillDetails.FindAsync(id);

            if (billDetail == null)
            {
                return NotFound();
            }

            return billDetail;
        }
    }
}
