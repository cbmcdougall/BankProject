using BankProject.BusinessLogicLayer;
using BankProject.BusinessLogicLayer.BLLContracts;
using BankProject.Entities;
using BankProject.Exceptions;
using BankProject.PresentationLayer;
using System;
using System.Collections.Generic;

namespace BankProject.Presentation
{
    /// <summary>
    /// Handles the UI of the Accounts presentation layer
    /// </summary>
    public class AccountsPresentation
    {
        #region Helper Methods
        /// <summary>
        /// Handles logging an exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="logType">Log the exception type as well as message</param>
        private static void LogException(Exception exception, bool logType = false)
        {
            if (logType)
            {
                Console.WriteLine($"{exception.GetType().Name}: {exception.Message}");
            }
            else
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Invokes the BLL to retrieve an account via an account code entered by the user.
        /// Raises an exception if the account code is invalid and allows entering 0 to cancel.
        /// </summary>
        /// <returns>The first matching account (null if cancelled)</returns>
        /// <exception cref="AccountException"></exception>
        private static Account GetAccountFromUser()
        {
            // Create BLL Object
            IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

            // Get the input from the user
            bool validInput = long.TryParse(Console.ReadLine(), out long accountCode);
            if (validInput && accountCode == 0) { return null; }

            // Find the matching account (pick first match)
            List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(account => account.AccountCode == accountCode);
            if (matchingAccounts.Count >= 1)
            {
                return matchingAccounts[0];
            }
            else
            {
                throw new AccountException("Invalid Account code");
            }
        }
        #endregion

        #region Accounts Menu Methods
        /// <summary>
        /// Handles the UI for adding an account
        /// </summary>
        internal static void AddAccount()
        {
            try
            {
                // Display all the current Customers
                if (CustomersPresentation.ViewCustomers())
                {
                    // Read the customer code to associate with the account
                    Console.Write("Enter the Customer Code of the customer you wish to create an account for: ");
                    long.TryParse(Console.ReadLine(), out long selectedCode);

                    // Create BLL Object
                    IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                    // Add the account
                    Guid newGuid = accountsBusinessLogicLayer.AddAccount(selectedCode);

                    // Get the account number that was generated to display to the user
                    List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(item => item.AccountID == newGuid);
                    if (matchingAccounts.Count > 0)
                    {
                        Console.WriteLine($"New Account added. Account Number: {matchingAccounts[0].AccountCode}");
                    }
                    else
                    {
                        Console.WriteLine("Account not added.");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot add an account, there are no existing customers.");
                }
            }
            catch (AccountException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Handles the UI for updating an account
        /// </summary>
        internal static void UpdateAccount()
        {
            try
            {
                // Get all accounts to display to the user
                if (ViewAccounts())
                {
                    // Get the account to update
                    Console.Write("Enter the Account Number of the Account you wish to update (0 to cancel): ");
                    Account accountToUpdate = GetAccountFromUser();
                    if (accountToUpdate == null) { return; }

                    // Display all customers to the user
                    if (CustomersPresentation.ViewCustomers())
                    {
                        // Get the updated customer code and name
                        Console.Write("Enter the updated Customer Code (press ENTER to keep the same code): ");
                        bool gotCode = long.TryParse(Console.ReadLine(), out long customerCode);
                        if (!gotCode) { customerCode = accountToUpdate.CustomerCode; }

                        // Create BLL Objects
                        IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();
                        ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                        // Attempt to find the customer
                        Customer associatedCustomer;
                        List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(customer => customer.CustomerCode == customerCode);
                        if (matchingCustomers.Count >= 1)
                        {
                            associatedCustomer = matchingCustomers[0];
                        }
                        else
                        {
                            throw new CustomerException("Invalid customer code.");
                        }

                        accountToUpdate.CustomerCode = associatedCustomer.CustomerCode;
                        accountToUpdate.CustomerName = associatedCustomer.CustomerName;

                        // Account balance
                        Console.Write("Balance: ");
                        bool validBalance = double.TryParse(Console.ReadLine(), out double updatedBalance);
                        if (!validBalance) { throw new CustomerException("Invalid balance amount."); }

                        accountToUpdate.Balance = updatedBalance;

                        // Update the account
                        if (accountsBusinessLogicLayer.UpdateAccount(accountToUpdate))
                        {
                            Console.WriteLine("Account Updated.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to update account.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no existing customers.");
                    }
                }
            }
            catch (AccountException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Handles the UI for deleting an account
        /// </summary>
        internal static void DeleteAccount()
        {
            try
            {
                // Get all accounts to display to the user
                if (ViewAccounts())
                {
                    // Get the account to delete
                    Console.Write("Enter the Account Number of the Account you wish to delete (0 to cancel): ");
                    Account accountToDelete = GetAccountFromUser();
                    if (accountToDelete == null) { return; }

                    // Create BLL Object
                    IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                    // Invoke the BLL to delete the account
                    if (accountsBusinessLogicLayer.DeleteAccount(accountToDelete.AccountID))
                    {
                        Console.WriteLine("Account Deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete account.");
                    }
                }
            }
            catch (AccountException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Handles the UI for searching for an account
        /// </summary>
        internal static void SearchAccounts()
        {
            try
            {
                // Create BLL Object
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                // Get the account code to search
                Console.WriteLine("\n****** SEARCH ACCOUNTS ******");

                bool validInput = false;
                long selectedCode;
                do
                {
                    Console.Write("Enter an Account Number to search for: ");
                    validInput = long.TryParse(Console.ReadLine(), out selectedCode);
                } while (!validInput);

                // Get matching accounts
                List<Account> matchingAccounts = accountsBusinessLogicLayer.GetAccountsByCondition(account => account.AccountCode == selectedCode);

                // Display account info
                if (matchingAccounts.Count >= 1)
                {
                    foreach (Account account in matchingAccounts)
                    {
                        Console.WriteLine($"\nAccount Number: {account.AccountCode}");
                        Console.WriteLine($"Customer Code: {account.CustomerCode}");
                        Console.WriteLine($"Customer Name: {account.CustomerName}");
                        Console.WriteLine($"Balance: {account.Balance:C}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo Accounts found.");
                }
            }
            catch (AccountException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Displays all the current existing accounts
        /// </summary>
        /// <returns>A boolean indicating if there are existing accounts to display</returns>
        internal static bool ViewAccounts()
        {
            try
            {
                // Create BLL Object
                IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                // Get all accounts
                List<Account> allAccounts = accountsBusinessLogicLayer.GetAccounts();

                if (allAccounts.Count > 0)
                {
                    // Display account info
                    Console.WriteLine("\n****** ALL ACCOUNTS ******");
                    foreach (Account account in allAccounts)
                    {
                        Console.WriteLine($"Account Number: {account.AccountCode}");
                        Console.WriteLine($"Customer Code: {account.CustomerCode}");
                        Console.WriteLine($"Customer Name: {account.CustomerName}");
                        Console.WriteLine($"Balance: {account.Balance:C}\n");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("\nThere are currently no accounts.");
                    return false;
                }
            }
            catch (AccountException ex)
            {
                LogException(ex);
                return false;
            }
            catch (Exception ex)
            {
                LogException(ex, true);
                return false;
            }
        }
        #endregion

        #region Transaction Methods
        /// <summary>
        /// Handles the UI for carrying out a transaction of funds between accounts
        /// </summary>
        internal static void FundTransfer()
        {
            try
            {
                // Display all accounts
                if (ViewAccounts())
                {
                    // Get the source account from the user
                    Console.Write("Enter the Source Account Number (0 to cancel): ");
                    Account sourceAccount = GetAccountFromUser();
                    if (sourceAccount == null) { return; }

                    // Get the destination account from the user
                    Console.Write("Enter the Destination Account Number (0 to cancel): ");
                    Account destinationAccount = GetAccountFromUser();
                    if (destinationAccount == null) { return; }
                    if (destinationAccount.AccountID == sourceAccount.AccountID)
                    {
                        throw new TransactionException("Cannot make a transfer to the same account.");
                    }

                    // Get the amount to transfer
                    Console.Write("Amount to transfer: ");
                    bool validAmount = double.TryParse(Console.ReadLine(), out double amount);
                    if (!validAmount) { throw new TransactionException("Invalid transfer amount."); }

                    // Create BLL object
                    IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                    // Invoke the BLL to carry out the funds transfer
                    if (accountsBusinessLogicLayer.FundTransfer(sourceAccount, destinationAccount, amount))
                    {
                        // Display resulting balances to the user
                        Console.WriteLine("Transaction successful.\n");
                        Console.WriteLine($"New balance for Account {sourceAccount.AccountCode}: {sourceAccount.Balance:C}");
                        Console.WriteLine($"New balance for Account {destinationAccount.AccountCode}: {destinationAccount.Balance:C}");
                    }
                    else
                    {
                        Console.WriteLine("Could not complete transaction.");
                    }
                }
            }
            catch (TransactionException ex)
            {
                LogException(ex);
            }
            catch (AccountException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Handles the UI for viewing an account's transactions statement
        /// </summary>
        internal static void AccountStatement()
        {
            try
            {
                // Display accounts to the user
                if (ViewAccounts())
                {
                    // Get the account to view from the user
                    Console.Write("Enter the Account number to view (0 to cancel): ");
                    Account accountToView = GetAccountFromUser();

                    // Create BLL object
                    IAccountsBusinessLogicLayer accountsBusinessLogicLayer = new AccountsBusinessLogicLayer();

                    // Invoke the BLL to retrieve the list of transactions on the account
                    List<Transaction> transactions = accountsBusinessLogicLayer.GetTransactions(accountToView);

                    // Display the account statement
                    Console.WriteLine($"\n**************** STATEMENT FOR ACCOUNT {accountToView.AccountCode} ****************");
                    Console.WriteLine(Transaction.StatementHeader());
                    transactions.ForEach(transaction => Console.WriteLine(transaction.ToString()));
                }
            }
            catch (TransactionException ex)
            {
                LogException(ex);
            }
            catch (AccountException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }
        #endregion
    }
}
