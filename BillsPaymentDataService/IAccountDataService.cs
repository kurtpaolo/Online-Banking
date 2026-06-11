using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public interface IAccountDataService
    {
        void Add(AccountModels account);
        List<AccountModels> GetAccounts();
        AccountModels? GetByUsernameAndPin(string createUsername, string createPIN);
        AccountModels? GetByUsername(string username);
        AccountModels? GetByReference(string referenceNumber);
        bool UsernameExists(string createUsername);
        bool Update(AccountModels account);
        bool RemoveByUsername(string username);
    }
}
