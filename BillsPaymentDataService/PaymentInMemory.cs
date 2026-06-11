using BillsPaymentModels;
using System.Security.Cryptography;

namespace BillsPaymentDataService
{
    public class PaymentInMemoryData : IPaymentDataService
    {
        private List<PaymentModels> payments = new();
        
        public void Add(PaymentModels payment)
        {
            payment.ReferenceNumber = GenerateReference(12);
            payment.DatePaid = DateTime.Now;
            payments.Add(payment);
        }

        public List<PaymentModels> GetPayments()
        {
            return payments;
        }

        public static string GenerateReference(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
            }

            return new string(result);
        }

        public PaymentModels? GetByReference(string reference)
        {
            return payments.FirstOrDefault(p => p.ReferenceNumber == reference);
        }

        public bool RemoveByReference(string referenceNumber)
        {
            var payment = payments.FirstOrDefault(p => p.ReferenceNumber == referenceNumber);

            if (payment == null)
                return false;

            payments.Remove(payment);
            return true;
        }

    }
}