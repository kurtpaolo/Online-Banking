using BillsPaymentModels;
using MySql.Data.MySqlClient;

namespace BillsPaymentDataService
{
    public class PaymentDBData : IPaymentDataService
    {
        private string connStr = "server=localhost;user=root;password=;database=billspayment;";
        private int referenceCounter = 0;

        public void Add(PaymentModels payment)
        {
            referenceCounter++;
            payment.ReferenceNumber = $"{referenceCounter:000000000}";

            using var conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "INSERT INTO payments VALUES (@id, @recipient, @amount, @datePaid, @reference)";
            var insertCommand = new MySqlCommand(sql, conn);

            payment.DatePaid = DateTime.Now;

            insertCommand.Parameters.AddWithValue("@id", payment.PaymentId.ToString());
            insertCommand.Parameters.AddWithValue("@recipient", payment.Recipient);
            insertCommand.Parameters.AddWithValue("@amount", payment.Amount);
            insertCommand.Parameters.AddWithValue("@datePaid", payment.DatePaid);
            insertCommand.Parameters.AddWithValue("@reference", payment.ReferenceNumber);
            insertCommand.ExecuteNonQuery();
        }

        public List<PaymentModels> GetPayments()
        {
            var list = new List<PaymentModels>();

            using var conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "SELECT * FROM payments";
            var insertCommand = new MySqlCommand(sql, conn);
            var reader = insertCommand.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new PaymentModels
                {
                    PaymentId = Guid.Parse(reader["PaymentId"].ToString()),
                    Recipient = reader["Recipient"].ToString(),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    DatePaid = Convert.ToDateTime(reader["DatePaid"]),
                    ReferenceNumber = reader["ReferenceNumber"].ToString()
                });
            }

            return list;
        }

        public PaymentModels? GetById(Guid paymentId)
        {
            return GetPayments().FirstOrDefault(p => p.PaymentId == paymentId);
        }
    }
}