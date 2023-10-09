using BankProject.BusinessLogicLayer.BLLContracts;
using BankProject.Configuration;
using BankProject.DataAccessLayer;
using BankProject.DataAccessLayer.DALContracts;
using BankProject.Entities;
using BankProject.Exceptions;
using System;
using System.Collections.Generic;

namespace BankProject.BusinessLogicLayer
{
    /// <summary>
    /// Represents account business logic
    /// </summary>
    public class AccountsBusinessLogicLayer : IAccountsBusinessLogicLayer
    {
        #region Private Fields
        private IAccountsDataAccessLayer _accountsDataAccessLayer;
        private ICustomersDataAccessLayer _customersDataAccessLayer;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that initialises the data access layers
        /// </summary>
        public AccountsBusinessLogicLayer()
        {
            _accountsDataAccessLayer = new AccountsDataAccessLayer();
            _customersDataAccessLayer = new CustomersDataAccessLayer();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Private property to represent a reference of AccountsDataAccessLayer
        /// </summary>
        private IAccountsDataAccessLayer AccountsDataAccessLayer
        {
            get => _accountsDataAccessLayer;
            set => _accountsDataAccessLayer = value;
        }
        /// <summary>
        /// Private property to represent a reference of CustomersDataAccessLayer
        /// </summary>
        private ICustomersDataAccessLayer CustomersDataAccessLayer
        {
            get => _customersDataAccessLayer;
            set => _customersDataAccessLayer = value;
        }
        #endregion

        #region Methods
        public List<Account> GetAccounts()
        {
            try
            {
                // Invoke the DAL to get accounts
                return AccountsDataAccessLayer.GetAccounts();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Account> GetAccountsByCondition(Predicate<Account> predicate)
        {
            try
            {
                // Invoke the DAL to get accounts
                return AccountsDataAccessLayer.GetAccountsByCondition(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddAccount(long customerCode)
        {
            try
            {
                // Create a new account object
                Account account = new Account();

                // Get all accounts
                List<Account> allAccounts = AccountsDataAccessLayer.GetAccounts();

                // Get the largest account code of all accounts, defaulting to the configured base account code
                long maxAccountCode = Settings.BaseAccountNo;
                allAccounts.ForEach(item => maxAccountCode = (item.AccountCode > maxAccountCode) ? item.AccountCode : maxAccountCode);

                // Generate new account code number
                account.AccountCode = maxAccountCode + 1;

                // Get the CustomerID of the customer of the provided customerCode
                List<Customer> matchingCustomers = CustomersDataAccessLayer.GetCustomersByCondition(item => item.CustomerCode == customerCode);
                if (matchingCustomers.Count > 0)
                {
                    Customer associatedCustomer = matchingCustomers[0];
                    account.CustomerCode = associatedCustomer.CustomerCode;
                    account.CustomerName = associatedCustomer.CustomerName;
                }
                else
                {
                    throw new AccountException("Invalid Customer Code.");
                }

                // Initialise the account balance and transactions list
                account.Balance = 0.0;
                account.Transactions = new List<Transaction>();

                // Invoke the DAL to add the account
                return AccountsDataAccessLayer.AddAccount(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAccount(Account account)
        {
            try
            {
                // Invoke the DAL to update the account
                return AccountsDataAccessLayer.UpdateAccount(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteAccount(Guid accountId)
        {
            try
            {
                // Invoke the DAL to delete the account
                return AccountsDataAccessLayer.DeleteAccount(accountId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool FundTransfer(Account sourceAccount, Account destAccount, double amount)
        {
            try
            {
                // Adjust the balances of the accounts
                sourceAccount.Balance -= amount;
                destAccount.Balance += amount;

                // Create the transaction objects
                // Transaction TO destination account
                Transaction outwardTransaction = new Transaction()
                {
                    TransactionDate = DateTime.Now,
                    TransactionType = "OUT",
                    TransactionAccount = destAccount.AccountCode,
                    TransactionAmount = amount,
                    RollingBalance = sourceAccount.Balance
                };
                // Transaction FROM source account
                Transaction inwardTransaction = new Transaction()
                {
                    TransactionDate = DateTime.Now,
                    TransactionType = "IN",
                    TransactionAccount = sourceAccount.AccountCode,
                    TransactionAmount = amount,
                    RollingBalance = destAccount.Balance
                };

                // Invoke the DAL to add the transactions to the accounts
                AccountsDataAccessLayer.AddTransaction(outwardTransaction, sourceAccount);
                AccountsDataAccessLayer.AddTransaction(inwardTransaction, destAccount);

                // Update the accounts
                bool outSuccess = AccountsDataAccessLayer.UpdateAccount(sourceAccount);
                bool inSuccess = AccountsDataAccessLayer.UpdateAccount(destAccount);

                // Return true if both accounts successfully updated
                return outSuccess && inSuccess;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Transaction> GetTransactions(Account account)
        {
            try
            {
                // Invoke the DAL to get accounts
                return AccountsDataAccessLayer.GetTransactions(account);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
