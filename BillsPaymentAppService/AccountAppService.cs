using BillsPaymentDataService;
using BillsPaymentModels;

namespace BillsPaymentAppService
{
    public class AccountAppService
    {
        private AccountDataService accountDataService = new AccountDataService(new AccountDBData());

        public AccountModels? ProcessAccounts(string username, string pin)
        {
            AccountModels account = new AccountModels
            {
                ReferenceNumber = Guid.NewGuid().ToString(),
                Username = username,
                PIN = pin
            };

            bool success = accountDataService.Add(account);

            return success ? account : null;
        }

        public List<AccountModels> GetAllAccounts()
        {
            return accountDataService.GetAccounts();
        }

        public AccountModels? GetAccount(string username, string pin)
        {
            return accountDataService.GetByUsernameAndPin(username, pin);
        }

        public AccountModels? GetAccountByUsername(string username)
        {
            return accountDataService.GetByUsername(username);
        }

        public AccountModels? GetAccountByReference(string referenceNumber)
        {
            return accountDataService.GetByReference(referenceNumber);
        }

        public bool UpdateAccount(string username, string pin)
        {
            AccountModels account = new AccountModels
            {
                Username = username,
                PIN = pin
            };

            return accountDataService.Update(account);
        }

        public bool RemoveAccount(string username)
        {
            return accountDataService.RemoveByUsername(username);
        }
    }
}


