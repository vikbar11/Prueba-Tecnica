namespace Billing.Api.DTOs
{
    public class BillDetailDTO
    {
        public int IdBillDetail { get; set; }
        public string? Description { get; set; }
        public int IdBill { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
    }
}
