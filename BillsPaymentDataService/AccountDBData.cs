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

            string sql = "INSERT INTO accounts (ReferenceNumber, Username, Pin) VALUES (@Ref, @Username, @Pin)";
            var insertCommand = new MySqlCommand(sql, conn);

            insertCommand.Parameters.AddWithValue("@Ref", account.ReferenceNumber);
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
                    ReferenceNumber = reader["ReferenceNumber"].ToString(),
                    Username = reader["Username"].ToString(),
                    PIN = reader["Pin"].ToString()
                };
            }

            return null;
        }

        public List<AccountModels> GetAccounts()
        {
            var list = new List<AccountModels>();

            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "SELECT * FROM accounts";
            var cmd = new MySqlCommand(sql, conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AccountModels
                {
                    ReferenceNumber = reader["ReferenceNumber"].ToString(),
                    Username = reader["Username"].ToString(),
                    PIN = reader["Pin"].ToString()
                });
            }

            return list;
        }

        public AccountModels? GetByUsername(string username)
        {
            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "SELECT * FROM accounts WHERE Username = @Username";

            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);

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

        public AccountModels? GetByReference(string referenceNumber)
        {
            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "SELECT * FROM accounts WHERE ReferenceNumber = @Ref";
            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Ref", referenceNumber);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AccountModels
                {
                    ReferenceNumber = reader["ReferenceNumber"].ToString(),
                    Username = reader["Username"].ToString(),
                    PIN = reader["Pin"].ToString()
                };
            }

            return null;
        }


        public bool UsernameExists(string username)
        {
            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "SELECT COUNT(*) FROM accounts WHERE Username = @Username";
            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Username", username);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool Update(AccountModels account)
        {
            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "UPDATE accounts SET Pin = @Pin WHERE Username = @Username";
            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Username", account.Username);
            cmd.Parameters.AddWithValue("@Pin", account.PIN);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool RemoveByUsername(string username)
        {
            using var conn = new MySqlConnection(connStrAccount);
            conn.Open();

            string sql = "DELETE FROM accounts WHERE Username = @Username";

            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}