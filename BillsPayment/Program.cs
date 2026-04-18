using BillsPaymentAppService;
using BillsPaymentModels;
using System;
using System.Security.Principal;

namespace BillsPayment
{
    class RedondoBillsPayment
    {
        static void Main(string[] args)
        {
            HomePage();
        }

        static void HomePage()
        {
            Console.WriteLine("Welcome to Redondo's Online Banking!");
            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Register");
            Console.WriteLine("[3] Enter Admin");
            Console.WriteLine("[4] Exit");
            Console.Write("Enter choice: ");
            string initialChoice = Console.ReadLine();

            switch (initialChoice)
            {
                case "1":
                    LoginPage();
                    break;
                case "2":
                    CreateAccount();
                    break;
                case "3":
                    AdminPage();
                    break;
                case "4":
                    Console.WriteLine("Thanks for using our service!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input!");
                    Environment.Exit(0);
                    break;

            }
            static void AdminPage()
            {
                Console.WriteLine("\nWelcome to the Admin Page!");
                Console.WriteLine("[1] View All Accounts");
                Console.WriteLine("[2] View All Payments");
                Console.WriteLine("[3] Home");
                Console.WriteLine("[3] Exit");
                Console.Write("Enter choice: ");
                string adminChoice = Console.ReadLine();
                switch (adminChoice)
                {
                    case "1":
                        var accountAppService = new AccountAppService();
                        var accounts = accountAppService.GetAllAccounts();
                        if (accounts.Count == 0)
                        {
                            Console.WriteLine("\nNo accounts found!");
                        }
                        else
                        {
                            Console.WriteLine("\nAccounts:");
                            foreach (var account in accounts)
                            {
                                Console.WriteLine($"Username: {account.Username}, PIN: {account.PIN}");
                            }

                            Console.WriteLine("\nProceed to:");
                            Console.WriteLine("[1] Home");
                            Console.WriteLine("[2] Exit");
                            Console.Write("Enter Choice: ");
                            string option = Console.ReadLine();
                            if (option == "1")
                            {
                                HomePage();
                            }
                            else if (option == "2")
                            {
                                Console.WriteLine("\nThanks for using our service!");
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid Input! Exiting...");
                                Environment.Exit(0);
                            }
                        }
                        break;
                    case "2":
                        var paymentAppService = new PaymentAppService();
                        var payments = paymentAppService.GetPaymentHistory();
                        if (payments.Count == 0)
                        {
                            Console.WriteLine("\nNo payments found!");
                        }
                        else
                        {
                            Console.WriteLine("\nPayments:");
                            foreach (var payment in payments)
                            {   
                                Console.WriteLine($"Recipient: {payment.Recipient}, Amount: {payment.Amount}, Date & Time: {payment.DatePaid}, Reference Number: {payment.ReferenceNumber}");
                            }
                        }

                        Console.WriteLine("\nProceed to:");
                        Console.WriteLine("[1] Home");
                        Console.WriteLine("[2] Exit");
                        Console.Write("Enter choice: ");
                        string options = Console.ReadLine();

                        if (options == "1")
                        {
                            HomePage();
                        }
                        else if (options == "2")
                        {
                            Console.WriteLine("Thanks for using our service!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input! Exiting...");
                            Environment.Exit(0);
                        }
                            break;
                    case "3":
                        HomePage();
                        break;
                    case "4":
                        Console.WriteLine("Thanks for using our service!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("Thank you for using our service!");
            }

            static void LoginPage()
            {

                var app = new AccountAppService();
                int attempts = 0, attemptsDisplay = 3;
                
                while (attempts < 3)
                {
                    Console.WriteLine("\nPlease Login to Proceed");
                    Console.Write("Username: ");
                    string userInput = Console.ReadLine();
                    Console.Write("PIN: ");
                    string pinInput = Console.ReadLine();
                    Console.WriteLine("");

                    var account = app.GetAccount(userInput, pinInput);

                    if (account != null)
                    {
                        PaymentProcess();
                        break;
                    }
                    else
                    {
                        attemptsDisplay--;
                        Console.WriteLine($"Incorrect Credentials! {attemptsDisplay} attempt/s left!");
                        attempts++;
                    }
                }

                if (attempts >= 3)
                    Console.WriteLine("Too many requests! Please try again later.");
                    HomePage();
            }

            static void CreateAccount()
            {

                var app = new AccountAppService();
                string username = "";
                string pin = "";

                Console.WriteLine("\nCreate Account");
                Console.Write("Username: ");
                username = Console.ReadLine();
                Console.Write("Create a PIN: ");
                pin = Console.ReadLine();
                Console.WriteLine();

                app.ProcessAccounts(username, pin);

                Console.WriteLine("Account Successfully Created!");
                Console.Write("Continue to Login? (y/n): ");
                string continueLogin = Console.ReadLine();

                if (continueLogin == "y")
                {   
                    
                    LoginPage();
                }
                else
                {
                    HomePage();
                }
            }

            static void PaymentProcess()
            {
                var app = new PaymentAppService();
                string morePayment = "y";

                while (morePayment == "y")
                {
                    Console.WriteLine("Hello! Would you like to: ");
                    Console.WriteLine("[1] Pay Bills");
                    Console.WriteLine("[2] Show Payment History");
                    Console.WriteLine("[3] Home");
                    Console.WriteLine("[4] Exit");
                    Console.Write("Enter choice: ");
                    string choice = Console.ReadLine();
                    Console.WriteLine("");

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
                                Console.WriteLine("\nMoney Transfer Successful!");
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
                                Console.WriteLine("\nNo payment history.");
                            }

                            else
                            {
                                Console.WriteLine("\nPayment History:");
                                foreach (var paymentHistory in history)
                                {
                                    Console.WriteLine($"----------------------------------------\nRecipient: {paymentHistory.Recipient}\nAmount: {paymentHistory.Amount}\nDate & Time: {paymentHistory.DatePaid}\nReference Number: {paymentHistory.ReferenceNumber}\n----------------------------------------\n");
                                }
                            }
                            break;
                        case "3":
                            HomePage();
                            break;
                        case "4":
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

                HomePage();
            }
        }
    }
}