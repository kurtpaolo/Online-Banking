using BillsPaymentModels;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace BillsPaymentDataService
{
    public class PaymentDBData : IPaymentDataService
    {
        private string connStr = "server=localhost;user=root;password=;database=billspayment;";

        public void Add(PaymentModels payment)
        {
            payment.ReferenceNumber = GenerateReference(12);

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

        public PaymentModels? GetByReference(string reference)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "SELECT * FROM payments WHERE ReferenceNumber = @ref";
            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ref", reference);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new PaymentModels
                {
                    Recipient = reader["Merchant"].ToString(),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    DatePaid = Convert.ToDateTime(reader["DatePaid"]),
                    ReferenceNumber = reader["ReferenceNumber"].ToString()
                };
            }

            return null;
        }

        public bool RemoveByReference(string referenceNumber)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            string sql = "DELETE FROM payments WHERE ReferenceNumber = @ReferenceNumber";

            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);

            return cmd.ExecuteNonQuery() > 0;
        }

        // Reference Number Maker
        public static string GenerateReference(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
            }
            return new string(result);
        }
    }
}