namespace _1293481Evidence.Models
{
    public class SaleItemHeadCount
    {
        public int InvoiceId { get; set; }
        public string ClientName { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string MedicineName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
