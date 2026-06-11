using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public class AccountInMemory : IAccountDataService
    {
        private List<AccountModels> accounts = new();

        public void Add(AccountModels account)
        {
            accounts.Add(account);
        }

        public List<AccountModels> GetAccounts()
        {
            return accounts;
        }
        public AccountModels? GetByUsernameAndPin(string username, string pin)
        {
            return accounts.FirstOrDefault(a => a.Username == username && a.PIN == pin);
        }

        public AccountModels? GetByReference(string referenceNumber)
        {
            return accounts.FirstOrDefault(a => a.ReferenceNumber == referenceNumber);
        }

        public AccountModels? GetByUsername(string username)
        {
            return accounts.FirstOrDefault(a => a.Username == username);
        }

        public bool UsernameExists(string username)
        {
            return accounts.Any(a => a.Username == username);
        }

        public bool Update(AccountModels account)
        {
            var existing = accounts.FirstOrDefault(a => a.Username == account.Username);

            if (existing == null)
                return false;

            existing.Username = account.Username;
            existing.PIN = account.PIN;

            return true;
        }

        public bool RemoveByUsername(string username)
        {
            var account = accounts.FirstOrDefault(a => a.Username == username);

            if (account == null)
                return false;

            accounts.Remove(account);
            return true;
        }
    }
}