using System;
namespace BillsPayment
{
    class RedondoBillsPayment
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {             
                string pin = "1214";
                Console.Write("PIN (Enter 1214 for the meantime): ");
                pin = Console.ReadLine();

                if (pin == "1214")
                {
                    ContinuedPayment();

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Thank you for using our service!");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("! ---------------------------------------");
                    Console.WriteLine("Incorrect PIN!");
                }
            }
            Console.Write("Too many requests!");
        }
        static void ContinuedPayment()
        {
            string morepayment = "y";
            while (morepayment == "y")
            {
                string recipient, paymentConfirmation = "", continued = "n";
                double amount;
                string reference = "(PLACEHOLDER)";
                DateTime dateToday = DateTime.Now;

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Welcome to Redondo's Online Banking!");
                Console.Write("Would you like to make a payment (y/n): ");
                continued = Console.ReadLine();

                if (continued == "y")
                {
                    {
                        Console.WriteLine("----------------------------------------");
                        Console.Write("Recipient: ");
                        recipient = Console.ReadLine();

                        Console.WriteLine("----------------------------------------");
                        Console.Write("Amount: ");
                        amount = double.Parse(Console.ReadLine());

                        Console.WriteLine("----------------------------------------");
                        Console.Write($"You are about to pay {recipient} an amount of {amount}. Do you want to proceed? (y/n): ");
                        paymentConfirmation = Console.ReadLine();

                        switch (paymentConfirmation)
                        {
                            case "y":
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine($"Your payment of PHP {amount} to {recipient} has been successfully");
                                Console.WriteLine($"processed on {dateToday}.");
                                Console.WriteLine($"Reference Number: {reference}");
                                Console.WriteLine("----------------------------------------");
                                break;

                            case "n":
                                Console.WriteLine("Payment cancelled.");
                                break;

                            default:
                                Console.WriteLine("Invalid input. Payment cancelled.");
                                break;
                        }

                    }
                }
                else
                {
                    break;
                }
                Console.Write("Continue? (y/n): ");
                morepayment = Console.ReadLine();
            }
        }
    }
}