using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public class AccountDataService
    {
        private IAccountDataService dataServices;

        public AccountDataService(IAccountDataService dataService)
        {
            dataServices = dataService;
        }

        public void Add(AccountModels account)
        {
            dataServices.Add(account);
        }

        public AccountModels? GetByUsernameAndPin(string username, string pin)
        {
            return dataServices.GetByUsernameAndPin(username, pin);
        }

        public List<AccountModels> GetAccounts()
        {
            return dataServices.GetAccounts();
        }
    }
}