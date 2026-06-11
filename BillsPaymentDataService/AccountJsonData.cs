using System.Text.Json;
using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public class AccountJsonData : IAccountDataService
    {
        private string fileName = "accounts.json";

        private List<AccountModels> Load()
        {
            if (!File.Exists(fileName))
                return new List<AccountModels>();

            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<AccountModels>>(json) ?? new();
        }

        private void Save(List<AccountModels> accounts)
        {
            var json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(fileName, json);
        }

        public void Add(AccountModels account)
        {
            var accounts = Load();
            accounts.Add(account);
            Save(accounts);
        }

        public List<AccountModels> GetAccounts()
        {
            return Load();
        }


        public AccountModels? GetByUsernameAndPin(string username, string pin)
        {
            var accounts = Load();
            return accounts.FirstOrDefault(a => a.Username == username && a.PIN == pin);
        }

        public AccountModels? GetByUsername(string username)
        {
            var accounts = Load();
            return accounts.FirstOrDefault(a => a.Username == username);
        }

        public AccountModels? GetByReference(string referenceNumber)
        {
            var accounts = Load();
            return accounts.FirstOrDefault(a => a.ReferenceNumber == referenceNumber);
        }

        public bool UsernameExists(string username)
        {
            var accounts = Load();
            return accounts.Any(a => a.Username == username);
        }

        public bool Update(AccountModels account)
        {
            var accounts = Load();

            var existing = accounts.FirstOrDefault(a => a.Username == account.Username);

            if (existing == null)
                return false;

            existing.Username = account.Username;
            existing.PIN = account.PIN;

            Save(accounts);
            return true;
        }

        public bool RemoveByUsername(string username)
        {
            var accounts = Load();

            var account = accounts.FirstOrDefault(a => a.Username == username);

            if (account == null)
                return false;

            accounts.Remove(account);
            Save(accounts);
            return true;
        }
    }
}