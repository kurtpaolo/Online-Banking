using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public interface IPaymentDataService
    {
        void Add(PaymentModels payment);
        List<PaymentModels> GetPayments();
        PaymentModels? GetByReference(string reference);
        bool RemoveByReference(string referenceNumber);
    }
}