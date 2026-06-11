using BillsPaymentAppService;
using BillsPaymentModels;
using System;

namespace BillsPayment
{
    class RedondoBillsPayment
    {
        static AccountAppService accountApp = new AccountAppService();
        static PaymentAppService paymentApp = new PaymentAppService();

        static string loggedInUser = "";

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nWelcome to Redondo's Online Banking!");
                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Register");
                Console.WriteLine("[3] Admin");
                Console.WriteLine("[4] Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
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
                        return;

                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
        }

        static void AdminPage()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Panel");
                Console.WriteLine("[1] View All Accounts");
                Console.WriteLine("[2] View All Payments");
                Console.WriteLine("[3] Delete Account");
                Console.WriteLine("[4] Delete Payment");
                Console.WriteLine("[5] Back");
                Console.WriteLine("[6] Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var accounts = accountApp.GetAllAccounts();

                        if (accounts.Count == 0)
                        {
                            Console.WriteLine("No accounts found.");
                        }
                        else
                        {
                            foreach (var a in accounts)
                            {
                                Console.WriteLine($"User: {a.Username} | Ref: {a.ReferenceNumber}");
                            }
                        }
                        break;

                    case "2":
                        var payments = paymentApp.GetPaymentHistory();

                        if (payments.Count == 0)
                        {
                            Console.WriteLine("No payments found.");
                        }
                        else
                        {
                            foreach (var p in payments)
                            {
                                Console.WriteLine($"Recipient: {p.Recipient} | Amount: {p.Amount} | Ref: {p.ReferenceNumber}");
                            }
                        }
                        break;

                    case "3":
                        Console.Write("Enter Username to delete: ");
                        string username = Console.ReadLine();

                        bool accDeleted = accountApp.RemoveAccount(username);

                        if (accDeleted == true)
                        {
                            Console.WriteLine("Account deleted successfully!");
                        }

                        else
                        {
                            Console.WriteLine("Account not found!");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Payment ReferenceNumber to delete: ");
                        string payRef = Console.ReadLine();

                        bool payDeleted = paymentApp.RemovePayment(payRef);

                        Console.WriteLine(payDeleted
                            ? "Payment deleted successfully!"
                            : "Payment not found.");
                        break;

                    case "5":
                        return;

                    case "6":
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
        }

        static void LoginPage()
        {
            int attempts = 0;

            while (attempts < 3)
            {
                Console.WriteLine("\nLogin");

                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("PIN: ");
                string pin = Console.ReadLine();

                var account = accountApp.GetAccount(username, pin);

                if (account != null)
                {
                    loggedInUser = username;
                    Console.WriteLine("Login successful!");
                    PaymentProcess();
                    return;
                }

                attempts++;
                Console.WriteLine($"Invalid credentials. Attempts left: {3 - attempts}");
            }

            Console.WriteLine("Too many failed attempts.");
        }

        static void CreateAccount()
        {
            Console.WriteLine("\nCreate Account");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("PIN: ");
            string pin = Console.ReadLine();

            var account = accountApp.ProcessAccounts(username, pin);

            if (account == null)
            {
                Console.WriteLine("Username already exists!");
                return;
            }

            Console.WriteLine("Account created successfully!");
            Console.WriteLine($"Reference: {account.ReferenceNumber}");
        }

        static void PaymentProcess()
        {
            while (true)
            {
                Console.WriteLine($"\nLogged in as: {loggedInUser}");
                Console.WriteLine("[1] Pay Bills");
                Console.WriteLine("[2] Show Payment History");
                Console.WriteLine("[3] Update PIN");
                Console.WriteLine("[4] Logout");
                Console.WriteLine("[5] Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PayBills();
                        break;

                    case "2":
                        ShowHistory();
                        break;

                    case "3":
                        UpdatePin();
                        break;

                    case "4":
                        loggedInUser = "";
                        Console.WriteLine("Logged out.");
                        return;

                    case "5":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void PayBills()
        {
            Console.Write("Recipient: ");
            string recipient = Console.ReadLine();

            Console.Write("Amount: ");
            int amount = int.Parse(Console.ReadLine());

            Console.Write("Confirm? (y/n): ");
            string confirm = Console.ReadLine();

            if (confirm == "y")
            {
                var payment = paymentApp.ProcessPayment(recipient, amount);

                Console.WriteLine("\nPayment Successful!");
                Console.WriteLine($"Ref: {payment.ReferenceNumber}");
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
        }

        static void ShowHistory()
        {
            var history = paymentApp.GetPaymentHistory();

            if (history.Count == 0)
            {
                Console.WriteLine("No history.");
                return;
            }

            foreach (var p in history)
            {
                Console.WriteLine($"Recipient: {p.Recipient} | Amount: {p.Amount} | Ref: {p.ReferenceNumber}");
            }
        }

        static void UpdatePin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("New PIN: ");
            string pin = Console.ReadLine();

            bool success = accountApp.UpdateAccount(username, pin);

            Console.WriteLine(success ? "Updated!" : "Account not found.");
        }
    }
}