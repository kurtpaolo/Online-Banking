using BillsPaymentModels;
using System.Text.Json;

namespace BillsPaymentDataService
{
    public class PaymentJsonData : IPaymentDataService
    {
        private string fileName = "payments.json";
        private int referenceCounter = 0;

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

            referenceCounter = 0;
            referenceCounter++;

            payment.ReferenceNumber = $"{referenceCounter:000000000}";
            payment.DatePaid = DateTime.Now;

            payments.Add(payment);
            Save(payments);
        }

        public List<PaymentModels> GetPayments()
        {
            return Load();
        }
    }
}