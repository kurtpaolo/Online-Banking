//using System;
//using System.Collections.Generic;
//using System.Linq;
using BillsPaymentModels;

namespace BillsPaymentDataService
{
    public class PaymentDataService
    {
        private IPaymentDataService dataServices;

        public PaymentDataService(IPaymentDataService dataService)
        {
            dataServices = dataService;
        }

        public void Add(PaymentModels payment)
        {
            dataServices.Add(payment);
        }

        public PaymentModels? GetById(Guid paymentId)
        {
            return dataServices.GetById(paymentId);
        }

        public List<PaymentModels> GetPayments()
        {
            return dataServices.GetPayments();
        }
    }
}