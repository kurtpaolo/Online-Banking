namespace BillsPaymentModels
{
    public class PaymentModels
    {
        public string Recipient { get; set; }
        public double Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public string ReferenceNumber { get; set; }
    }
}