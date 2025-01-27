using System;

namespace AbstractionAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            ATMSystem atm = new ATMSystem();

            Console.WriteLine("Welcome to Legits ATM");
            Console.Write("Enter your card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter your PIN: ");
            string pin = Console.ReadLine();

            string sessionId = atm.AuthenticateUser(cardNumber, pin);

            if (string.IsNullOrEmpty(sessionId))
            {
                Console.WriteLine("Incorrect login. Exiting...");
                return;
            }

            Console.WriteLine("Login successful. Session started.");

            bool isRunning = true;
            while (isRunning && atm.IsSessionActive(sessionId))
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        decimal balance = atm.GetBalance(cardNumber);
                        Console.WriteLine($"Your balance is: ${balance:F2}");
                        break;

                    case "2":
                        Console.Write("Enter the amount to deposit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount) && depositAmount > 0)
                        {
                            atm.PerformBankOperation(OperationType.Deposit, cardNumber, depositAmount);
                            Console.WriteLine("Deposit successful.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Please try again.");
                        }
                        break;

                    case "3":
                        Console.Write("Withdrawal amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount) && withdrawAmount > 0)
                        {
                            if (atm.AuthorizeTransaction(cardNumber, withdrawAmount))
                            {
                                atm.PerformBankOperation(OperationType.Withdraw, cardNumber, withdrawAmount);
                                Console.WriteLine("Withdrawal successful.");
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Please try again.");
                        }
                        break;

                    case "4":
                        isRunning = false;
                        atm.EndSession(sessionId);
                        Console.WriteLine("Session ended. Thank you for using the ATM System.");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            if (!atm.IsSessionActive(sessionId))
            {
                Console.WriteLine("Session expired. Please log in again.");
            }
        }
    }
}

