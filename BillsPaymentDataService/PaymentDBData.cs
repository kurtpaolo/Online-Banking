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

            string sql = "INSERT INTO payments VALUES (@Merchant, @Amount, @DatePaid, @Reference)";
            var insertCommand = new MySqlCommand(sql, conn);

            payment.DatePaid = DateTime.Now;

            insertCommand.Parameters.AddWithValue("@Merchant", payment.Recipient);
            insertCommand.Parameters.AddWithValue("@Amount", payment.Amount);
            insertCommand.Parameters.AddWithValue("@DatePaid", payment.DatePaid);
            insertCommand.Parameters.AddWithValue("@Reference", payment.ReferenceNumber);
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
                    Recipient = reader["Merchant"].ToString(),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    DatePaid = Convert.ToDateTime(reader["DatePaid"]),
                    ReferenceNumber = reader["ReferenceNumber"].ToString()
                });
            }

            return list;
        }
    }
}