//using System;
//using System.Collections.Generic;
using BillsPaymentModels;
using BillsPaymentDataService;

namespace BillsPaymentAppService
{
    public class PaymentAppService
    {
        private PaymentDataService paymentDataService = new PaymentDataService(new PaymentJsonData());

        public PaymentModels ProcessPayment(string recipient, int amount)
        {
            PaymentModels payment = new PaymentModels
            {
                Recipient = recipient,
                Amount = amount,
            };

            paymentDataService.Add(payment);
            return payment;
        }

        public List<PaymentModels> GetPaymentHistory()
        {
            return paymentDataService.GetPayments();
        }
    }
}


