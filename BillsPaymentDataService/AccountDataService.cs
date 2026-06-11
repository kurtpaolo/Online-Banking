using BillsPaymentModels;
using System.Runtime.Serialization.Json;

namespace BillsPaymentDataService
{
    public class AccountDataService
    {
        private IAccountDataService dataServices;

        public AccountDataService(IAccountDataService dataService)
        {
            dataServices = dataService;
        }

        public bool Add(AccountModels account)
        {
            if (dataServices.UsernameExists(account.Username))
            {
                return false;
            }

            dataServices.Add(account);
            return true;
        }

        public List<AccountModels> GetAccounts()
        {
            return dataServices.GetAccounts();
        }
        public AccountModels? GetByUsernameAndPin(string username, string pin)
        {
            return dataServices.GetByUsernameAndPin(username, pin);
        }
        public AccountModels? GetByUsername(string username)
        {
            return dataServices.GetByUsername(username);
        }

        public AccountModels? GetByReference(string referenceNumber)
        {
            return dataServices.GetByReference(referenceNumber);
        }

        public bool UsernameExists(string username)
        {
            return dataServices.UsernameExists(username);
        }

        public bool Update(AccountModels account)
        {
            return dataServices.Update(account);
        }
        public bool RemoveByUsername(string referenceNumber)
        {
            return dataServices.RemoveByUsername(referenceNumber);
        }
    }
}