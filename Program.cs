using System;
namespace BillsPayment
{
    class RedondoBillsPayment
    {
        static void Main(string[] args)
        {
            string recipient, paymentConfirmation = "", continued = "n";
            double amount;
            int reference = 1000000;
            DateTime dateToday = DateTime.Now;

                Console.Write("Would you like to make a payment (y/n): ");
                continued = Console.ReadLine();

            if (continued == "y")
            {
                {
                    Console.Write("Recipient: ");
                    recipient = Console.ReadLine();
                    Console.Write("Amount: ");
                    amount = double.Parse(Console.ReadLine());

                    Console.Write($"You are about to pay {recipient} an amount of {amount}. Do you want to proceed? (y/n): ");
                    paymentConfirmation = Console.ReadLine();

                    switch (paymentConfirmation)
                    {
                        case "y":
                            Console.WriteLine($"Your payment of PHP {amount} to");
                            Console.WriteLine($"{recipient} has been successfully");
                            Console.WriteLine($"processed on {dateToday}.");
                            Console.WriteLine($"Reference Number: {reference++}");
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
                Console.WriteLine("Goodbye!");
        }
    }
}