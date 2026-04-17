using BillsPaymentDataService;
using BillsPaymentModels;

namespace BillsPaymentAppService
{
    public class AccountAppService
    {
        private AccountDataService accountDataService = new AccountDataService(new AccountDBData());

        public AccountModels ProcessAccounts(string username, string pin)
        {
            AccountModels account = new AccountModels
            {
                Username = username,
                PIN = pin
            };

            accountDataService.Add(account);
            return account;
        }

        public AccountModels? GetAccount(string username, string pin)
        {
            return accountDataService.GetByUsernameAndPin(username, pin);
        }
    }
}


