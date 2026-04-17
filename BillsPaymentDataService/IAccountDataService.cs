using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public interface IAccountDataService
    {
        void Add(AccountModels account);
        AccountModels? GetByUsernameAndPin(string createUsername, string createPIN);
    }
}
