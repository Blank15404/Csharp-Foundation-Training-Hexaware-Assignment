using System.Text.RegularExpressions;

namespace Csharp_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. BANK LOAN ELIGIBILITY CHECK
            Console.WriteLine("=== BANK LOAN ELIGIBILITY CHECK ===");

            Console.WriteLine("Enter your credit score:");
            int creditScore = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter your annual income ($):");
            double annualIncome = Convert.ToDouble(Console.ReadLine());

            if (creditScore > 700 && annualIncome >= 50000)
            {
                Console.WriteLine("Congratulations! You are eligible for a loan.");
            }
            else
            {
                Console.WriteLine("Sorry, you are not eligible for a loan.");
                if (creditScore <= 700)
                {
                    Console.WriteLine($"\t- Your credit score is {creditScore}, which needs to be above 700.");
                }
                if (annualIncome < 50000)
                {
                    Console.WriteLine($"\t- Your annual income is ${annualIncome}, which needs to be at least $50,000.");
                }
            }

            //2. ATM TRANSACTION SIMULATION
            Console.WriteLine("\n=== ATM SIMULATION ===");

            Console.Write("Enter your current balance: ");
            double balance = Convert.ToDouble(Console.ReadLine());

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Withdraw Money");
                Console.WriteLine("3. Deposit Money");
                Console.WriteLine("4. Exit");
                Console.Write("\nEnter your choice (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"\nYour current balance is: {balance:C}");
                        break;

                    case "2":
                        Console.Write("\nEnter amount to withdraw (multiples of 100 or 500):");
                        double withdrawAmount = Convert.ToDouble(Console.ReadLine());

                        if (withdrawAmount > balance)
                        {
                            Console.WriteLine("Insufficient funds!");
                        }
                        else if (withdrawAmount % 100 != 0 && withdrawAmount % 500 != 0)
                        {
                            Console.WriteLine("Amount must be in multiples of 100 or 500!");
                        }
                        else
                        {
                            balance -= withdrawAmount;
                            Console.WriteLine($"Withdrawal successful. New balance: {balance:C}");
                        }
                        break;

                    case "3":
                        Console.Write("\nEnter amount to deposit: ");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        balance += depositAmount;
                        Console.WriteLine($"Deposit successful. New balance: {balance:C}");
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("\nThank you for using our ATM. Have a Great Day!");
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice! Please enter 1, 2, 3 or 4.");
                        break;
                }
            }

            //3. COMPOUND INTEREST CALCULATOR
            Console.WriteLine("\n=== COMPOUND INTEREST CALCULATOR ===");

            Console.WriteLine("How many customers would you like to calculate for?");
            int customerCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= customerCount; i++)
            {
                Console.WriteLine($"\nCustomer {i}:");

                Console.WriteLine("Enter initial balance:");
                double initialBalance = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter annual interest rate (%):");
                double annualInterestRate = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter number of years:");
                int years = Convert.ToInt32(Console.ReadLine());

                double futureBalance = initialBalance * Math.Pow(1 + annualInterestRate / 100, years);

                Console.WriteLine($"\nFuture balance after {years} years: {futureBalance:C}");
            }

            //4. SIMPLE ACCOUNT BALANCE CHECKER
            Console.WriteLine("\n=== SIMPLE ACCOUNT BALANCE CHECKER ===");
            Console.Write("How many accounts would you like to create? ");
            int accountCount = Convert.ToInt32(Console.ReadLine());

            string[] accountNumbers = new string[accountCount];
            double[] accountBalances = new double[accountCount];

            // Let user enter account details
            for (int i = 0; i < accountCount; i++)
            {
                Console.WriteLine($"\nEnter details for Account {i + 1}:");

                Console.Write("Account Number (INDB followed by 4 digits, e.g., INDB1234): ");
                accountNumbers[i] = Console.ReadLine().ToUpper();

                Console.Write("Account Balance: ");
                accountBalances[i] = Convert.ToDouble(Console.ReadLine());
            }

            bool checking = true;
            while (checking)
            {
                Console.WriteLine("\nEnter account number to check balance (or type 'exit' to quit):");
                string input = Console.ReadLine().ToUpper();

                if (input == "EXIT")
                {
                    checking = false;
                    Console.WriteLine("Thank you for using our service!");
                    continue;
                }

                bool found = false;
                for (int i = 0; i < accountCount; i++)
                {
                    if (accountNumbers[i] == input)
                    {
                        Console.WriteLine($"Balance for account {input}: {accountBalances[i]:C}");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Account not found. Please try again.");
                }
            }

            //5. PASSWORD VALIDATOR
            Console.WriteLine("\n=== PASSWORD VALIDATOR ===");
            Console.WriteLine("\nCreate a password for your bank account with these rules:");
            Console.WriteLine("\t- At least 8 characters long");
            Console.WriteLine("\t- At least one uppercase letter");
            Console.WriteLine("\t- At least one digit");

            bool isValidPassword = false;

            while (!isValidPassword)
            {
                Console.WriteLine("\nEnter your password:");
                string password = Console.ReadLine();

                if (password.Length < 8)
                {
                    Console.WriteLine("Password must be at least 8 characters long.");
                }
                else if (!Regex.IsMatch(password, "[A-Z]"))
                {
                    Console.WriteLine("Password must contain at least one uppercase letter.");
                }
                else if (!Regex.IsMatch(password, "[0-9]"))
                {
                    Console.WriteLine("Password must contain at least one digit.");
                }
                else
                {
                    isValidPassword = true;
                    Console.WriteLine("Password is valid and has been set successfully!");
                }
            }

            //6. TRANSACTION HISTORY
            Console.WriteLine("\n=== TRANSACTION HISTORY ===");

            List<string> transactions = new List<string>();
            double balance1 = 0;
            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. View Transactions");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice (1-4):");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter deposit amount:");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        balance1 += depositAmount;
                        string depositRecord = $"{DateTime.Now}: Deposit +{depositAmount:C}";
                        transactions.Add(depositRecord);
                        Console.WriteLine("Deposit successful.");
                        break;

                    case 2:
                        Console.WriteLine("Enter withdrawal amount:");
                        double withdrawAmount = Convert.ToDouble(Console.ReadLine());

                        if (withdrawAmount > balance1)
                        {
                            Console.WriteLine("Insufficient funds!");
                        }
                        else
                        {
                            balance1 -= withdrawAmount;
                            string withdrawRecord = $"{DateTime.Now}: Withdraw -{withdrawAmount:C}";
                            transactions.Add(withdrawRecord);
                            Console.WriteLine("Withdrawal successful.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("\n=== TRANSACTION HISTORY ===");
                        Console.WriteLine($"Current Balance: {balance1:C}\n");

                        foreach (string transaction in transactions)
                        {
                            Console.WriteLine(transaction);
                        }
                        break;

                    case 4:
                        exitProgram = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }

            Console.WriteLine("\nThank you for using our banking services!");
        }
    }
}
