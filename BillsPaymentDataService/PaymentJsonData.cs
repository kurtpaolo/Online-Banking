using BillsPaymentModels;
using System.Security.Cryptography;
using System.Text.Json;

namespace BillsPaymentDataService
{
    public class PaymentJsonData : IPaymentDataService
    {
        private string fileName = "payments.json";

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

        private List<PaymentModels> Load()
        {
            if (!File.Exists(fileName))
                return new List<PaymentModels>();

            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<PaymentModels>>(json) ?? new List<PaymentModels>();
        }

        private void Save(List<PaymentModels> payments)
        {
            string json = JsonSerializer.Serialize(payments, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public void Add(PaymentModels payment)
        {
            var payments = Load();

            payment.ReferenceNumber = GenerateReference(12);
            payment.DatePaid = DateTime.Now;

            payments.Add(payment);
            Save(payments);
        }

        public List<PaymentModels> GetPayments()
        {
            return Load();
        }

        public PaymentModels? GetByReference(string reference)
        {
            var payments = Load();
            return payments.FirstOrDefault(p => p.ReferenceNumber == reference);
        }

        public bool RemoveByReference(string referenceNumber)
        {
            var payments = Load();

            var payment = payments.FirstOrDefault(p => p.ReferenceNumber == referenceNumber);

            if (payment == null)
                return false;

            payments.Remove(payment);
            Save(payments);
            return true;
        }
    }
}