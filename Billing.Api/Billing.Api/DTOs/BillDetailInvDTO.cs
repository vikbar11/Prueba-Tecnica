namespace Billing.Api.DTOs
{
    public class BillDetailInvDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = null!;
    }
}
