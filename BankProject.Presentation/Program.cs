using BankProject.Presentation;
using System;

namespace BankProject.PresentationLayer
{
    class Program
    {
        static void Main()
        {
            // Display title
            Console.WriteLine("************** Bank of Holding **************");
            Console.WriteLine("::Login Page::");

            // Declare username and password variables
            string username, password = null;

            // Read username from user input
            Console.Write("Username (press ENTER to cancel): ");
            username = Console.ReadLine();


            // Read password from user input only if username is entered
            if (!string.IsNullOrWhiteSpace(username))
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }
            else
            {
                ExitProgram();
            }

            // Validate login details
            if (username == "system" && password == "manager")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }

            ExitProgram();
        }

        /// <summary>
        /// Prompts the user to input a menu choice, until they enter a valid choice
        /// </summary>
        /// <param name="maxOption">The max number of menu options</param>
        /// <param name="minOption">The smallest allowed menu option number</param>
        /// <returns>An integer representing the user's menu choice</returns>
        internal static int GetMenuChoice(int maxOption, int minOption = 0)
        {
            int choice;
            do
            {
                // Clear previous line
                Console.CursorTop--;
                Console.Write($"\r{new string(' ', Console.BufferWidth - 1)}\r");

                // Attempt to get user's choice
                Console.Write("Enter choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice)) { choice = minOption - 1; }
            } while (choice < minOption || choice > maxOption);

            return choice;
        }

        /// <summary>
        /// Handles the UI for the Main menu
        /// </summary>
        internal static void MainMenu()
        {
            // Display main menu
            Console.WriteLine("\n:::Main menu:::");
            Console.WriteLine("1. Customers");
            Console.WriteLine("2. Accounts");
            Console.WriteLine("3. Funds Transfer");
            Console.WriteLine("4. Account Statement");
            Console.WriteLine("0. Exit\n");

            // Get user's menu choice
            switch (GetMenuChoice(4))
            {
                case 1: CustomersMenu(); break;
                case 2: AccountsMenu(); break;
                case 3: AccountsPresentation.FundTransfer(); MainMenu(); break;
                case 4: AccountsPresentation.AccountStatement(); MainMenu(); break;
                case 0: ExitProgram(); break;
            }
        }

        /// <summary>
        /// Handles the UI for the Customers menu
        /// </summary>
        internal static void CustomersMenu()
        {
            Console.WriteLine("\n:::Customers Menu:::");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. Update Customer");
            Console.WriteLine("3. Delete Customer");
            Console.WriteLine("4. Search Customers");
            Console.WriteLine("5. View Customers");
            Console.WriteLine("0. Back to Main Menu\n");

            switch (GetMenuChoice(5))
            {
                case 1: CustomersPresentation.AddCustomer(); break;
                case 2: CustomersPresentation.UpdateCustomer(); break;
                case 3: CustomersPresentation.DeleteCustomer(); break;
                case 4: CustomersPresentation.SearchCustomers(); break;
                case 5: CustomersPresentation.ViewCustomers(); Console.CursorTop--; break;
                case 0: MainMenu(); break;
            }

            // Return the user to this menu unless they've returned to main
            CustomersMenu();
        }

        /// <summary>
        /// Handles the UI for the Accounts menu
        /// </summary>
        internal static void AccountsMenu()
        {
            Console.WriteLine("\n:::Accounts Menu:::");
            Console.WriteLine("1. Add Account");
            Console.WriteLine("2. Update Account");
            Console.WriteLine("3. Delete Account");
            Console.WriteLine("4. Search Accounts");
            Console.WriteLine("5. View Accounts");
            Console.WriteLine("0. Back to Main Menu\n");

            switch (GetMenuChoice(5))
            {
                case 1: AccountsPresentation.AddAccount(); break;
                case 2: AccountsPresentation.UpdateAccount(); break;
                case 3: AccountsPresentation.DeleteAccount(); break;
                case 4: AccountsPresentation.SearchAccounts(); break;
                case 5: AccountsPresentation.ViewAccounts(); Console.CursorTop--; break;
                case 0: MainMenu(); break;
            }

            // Return the user to this menu unless they've returned to main
            AccountsMenu();
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="exitCode">The exit status code to return to the system</param>
        internal static void ExitProgram(int exitCode = 0)
        {
            Console.WriteLine("\nThank you for using our services, we hope you come again!");
            Console.ReadKey();
            Environment.Exit(exitCode);
        }
    }
}