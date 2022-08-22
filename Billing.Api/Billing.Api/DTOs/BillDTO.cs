namespace Billing.Api.DTOs
{
    public class BillDTO
    {
        public int IdBill { get; set; }
        public int IdCustomer { get; set; }
        public DateTime BillDate { get; set; }

    }
}
