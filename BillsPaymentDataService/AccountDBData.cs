using BillsPaymentModels;
using MySql.Data.MySqlClient;

namespace BillsPaymentDataService
{
    public class AccountDBData : IAccountDataService
    {
        private string connStrAccount = "server=localhost;user=root;password=;database=billspayment;";

        public void Add(AccountModels account)
        {

            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "INSERT INTO accounts VALUES (@Username, @Pin)";
            var insertCommand = new MySqlCommand(sql, conn);

            insertCommand.Parameters.AddWithValue("@Username", account.Username);
            insertCommand.Parameters.AddWithValue("@Pin", account.PIN);
            insertCommand.ExecuteNonQuery();
        }

        public AccountModels? GetByUsernameAndPin(string username, string pin)
        {
            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "SELECT * FROM accounts WHERE Username = @Username AND Pin = @Pin";

            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Pin", pin);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AccountModels
                {
                    Username = reader["Username"].ToString(),
                    PIN = reader["Pin"].ToString()
                };
            }

            return null;
        }

        public List<AccountModels> GetAccounts()
        {
            var accounts = new List<AccountModels>();
            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();
            string sql = "SELECT * FROM accounts";
            var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                accounts.Add(new AccountModels
                {
                    Username = reader["Username"].ToString(),
                    PIN = reader["Pin"].ToString()
                });
            }
            return accounts;
        }
    }
}
