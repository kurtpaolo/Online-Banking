//using System;
//using System.Collections.Generic;
using BillsPaymentModels;
using BillsPaymentDataService;

namespace BillsPaymentAppService
{
    public class PaymentAppService
    {
        private PaymentDataService paymentDataService = new PaymentDataService(new PaymentDBData());

        public PaymentModels ProcessPayment(string recipient, int amount)
        {
            PaymentModels payment = new PaymentModels
            {
                Recipient = recipient,
                Amount = amount,
                DatePaid = DateTime.Now,
            };

            paymentDataService.Add(payment);
            return payment;
        }

        public List<PaymentModels> GetPaymentHistory()
        {
            return paymentDataService.GetPayments();
        }

        public PaymentModels? GetPaymentByReference(string reference)
        {
            return paymentDataService.GetByReference(reference);
        }

        public bool RemovePayment(string referenceNumber)
        {
            return paymentDataService.RemoveByReference(referenceNumber);
        }
    }
}


