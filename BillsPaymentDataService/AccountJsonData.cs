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

        public AccountModels? GetByUsernameAndPin(string username, string pin)
        {
            var accounts = Load();
            return accounts.FirstOrDefault(a => a.Username == username && a.PIN == pin);
        }
    }
}