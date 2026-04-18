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

        public AccountModels? GetByUsernameAndPin(string username, string pin)
        {
            return accounts.FirstOrDefault(a => a.Username == username && a.PIN == pin);
        }

        public List<AccountModels> GetAccounts()
        {
            return accounts;
        }
    }
}