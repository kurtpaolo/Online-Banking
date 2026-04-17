using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public interface IPaymentDataService
    {
        void Add(PaymentModels payment);
        List<PaymentModels> GetPayments();
    }
}