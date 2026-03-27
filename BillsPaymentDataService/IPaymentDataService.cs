using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public interface IPaymentDataService
    {
        void Add(PaymentModels payment);
        PaymentModels? GetById(Guid paymentId);
        List<PaymentModels> GetPayments();
    }
}