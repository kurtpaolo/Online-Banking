using System;
using BillsPaymentAppService;
using BillsPaymentModels;

namespace BillsPayment
{
    class RedondoBillsPayment
    {
        static void Main(string[] args)
        {
            CreateAccount();

            var app = new AccountAppService();
            int attempts = 0;    
            while (attempts < 3)
            {
                Console.Write("Username: ");
                string userInput = Console.ReadLine();
                Console.Write("PIN: ");
                string pinInput = Console.ReadLine();  
                
                var account = app.GetAccount(userInput, pinInput);

                if (account != null)
                {
                    PaymentProcess();
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect Credentials!");
                    attempts++;
                }
            }

            if (attempts >= 3)
                Console.WriteLine("Too many requests! Please try again later.");
                Environment.Exit(0);
        }

        static void CreateAccount() {

            var app = new AccountAppService();
            string username = "";
            string pin = "";

            Console.WriteLine("Create Account");
            Console.Write("Username: ");
            username = Console.ReadLine();    
            Console.Write("Create a PIN: ");
            pin = Console.ReadLine();

            app.ProcessAccounts(username, pin);
        }

        static void PaymentProcess()
        {
            var app = new PaymentAppService();
            string morePayment = "y";

            while (morePayment == "y")
            {
                Console.WriteLine("Welcome to Redondo's Online Banking!");
                Console.WriteLine("[1] Pay Bills");
                Console.WriteLine("[2] Show Payment History");
                Console.WriteLine("[3] Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Recipient: ");
                        string recipient = Console.ReadLine();

                        Console.Write("Amount: ");
                        int amount = int.Parse(Console.ReadLine());

                        Console.Write($"Pay {recipient} {amount}? Proceed? (y/n): ");
                        string confirm = Console.ReadLine();

                        if (confirm == "y")
                        {
                            PaymentModels payment = app.ProcessPayment(recipient, amount);
                            Console.WriteLine("Money Transfer Successful!");
                            Console.WriteLine($"----------------------------------------\nRecipient: {payment.Recipient}\nAmount: {payment.Amount}\nDate & Time: {payment.DatePaid}\nReference Number: {payment.ReferenceNumber}\n----------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Payment cancelled.");
                        }
                        break;

                    case "2":
                        var history = app.GetPaymentHistory();
                        if (history.Count == 0)
                        {
                            Console.WriteLine("No payment history.");
                        }

                        else
                        {
                            Console.WriteLine("Payment History:");
                            foreach (var paymentHistory in history)
                            {
                                Console.WriteLine($"----------------------------------------\nRecipient: {paymentHistory.Recipient}\nAmount: {paymentHistory.Amount}\nDate & Time: {paymentHistory.DatePaid}\nReference Number: {paymentHistory.ReferenceNumber}\nd----------------------------------------\n");
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("Thanks for using our service!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid Input!");
                        Environment.Exit(0);
                        break;
                }

                Console.Write("Continue? (y/n): ");
                morePayment = Console.ReadLine();
            }

            Console.WriteLine("Thank you for using our service!");
        }
    }
}