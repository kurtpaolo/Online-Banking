using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public class PaymentInMemoryData : IPaymentDataService
    {
        private List<PaymentModels> storedPayments = new List<PaymentModels>();
        private int referenceCounter = 0;

        public void Add(PaymentModels payment)
        {
            referenceCounter++;
            payment.ReferenceNumber = $"{referenceCounter:000000000}";
            payment.DatePaid = DateTime.Now;

            storedPayments.Add(payment);
        }

        public PaymentModels? GetById(Guid paymentId)
        {
            return storedPayments.FirstOrDefault(p => p.PaymentId == paymentId);
        }

        public List<PaymentModels> GetPayments()
        {
            return storedPayments;
        }
    }
}