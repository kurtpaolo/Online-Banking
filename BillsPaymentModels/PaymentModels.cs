using System;

namespace BillsPaymentModels
{
    public class PaymentModels
    {
        public Guid PaymentId { get; set; }
        public string Recipient { get; set; }
        public double Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public string ReferenceNumber { get; set; }

        public PaymentModels()
        {
            PaymentId = Guid.NewGuid();
        }
    }
}