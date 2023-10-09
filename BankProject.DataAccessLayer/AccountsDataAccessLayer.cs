using BankProject.DataAccessLayer.DALContracts;
using BankProject.Entities;
using System;
using System.Collections.Generic;

namespace BankProject.DataAccessLayer
{
    /// <summary>
    /// Represents the DAL for bank accounts
    /// </summary>
    public class AccountsDataAccessLayer : IAccountsDataAccessLayer
    {
        #region Fields
        private static List<Account> _accounts;
        #endregion

        #region Constructors
        static AccountsDataAccessLayer()
        {
            _accounts = new List<Account>();
        }
        #endregion

        #region Properties
        private static List<Account> Accounts
        {
            get => _accounts;
            set => _accounts = value;
        }
        #endregion

        #region Methods
        public List<Account> GetAccounts()
        {
            try
            {
                // Create a new Accounts list
                List<Account> accounts = new List<Account>();

                // Copy all accounts from the source collection into the new Accounts list
                Accounts.ForEach(item => accounts.Add(item.Clone() as Account));
                return accounts;
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
                // Create a new Accounts list
                List<Account> accountsList = new List<Account>();

                // Filter the collection
                List<Account> filteredAccounts = Accounts.FindAll(predicate);

                // Copy all accounts from the filtered collection into the new Accounts list
                filteredAccounts.ForEach(item => accountsList.Add(item.Clone() as Account));
                return accountsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddAccount(Account account)
        {
            try
            {
                // Generate new Guid
                account.AccountID = Guid.NewGuid();

                // Add the account to the collection
                Accounts.Add(account);
                return account.AccountID;
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
                // Find the existing account by AccountID
                Account existingAccount = Accounts.Find(item => item.AccountID == account.AccountID);

                // Update the account details
                if (existingAccount != null)
                {
                    existingAccount.CustomerCode = account.CustomerCode;
                    existingAccount.CustomerName = account.CustomerName;
                    existingAccount.Balance = account.Balance;
                    existingAccount.Transactions = account.Transactions;

                    return true; // Account is updated
                }
                else
                {
                    return false; // Account could not be found/updated
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteAccount(Guid accountID)
        {
            try
            {
                return Accounts.RemoveAll(item => item.AccountID == accountID) > 0; // True if account was successfully deleted
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddTransaction(Transaction transaction, Account account)
        {
            try
            {
                // Generate new Guid
                transaction.TransactionID = Guid.NewGuid();

                // Add the transaction to the account
                account.Transactions.Add(transaction);
                return transaction.TransactionID;
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
                // Create a new Transactions list
                List<Transaction> transactions = new List<Transaction>();

                // Get the account to retrieve transactions from
                Account existingAccount = Accounts.Find(item => item.AccountID == account.AccountID);

                // Copy all accounts from the source collection into the new Transactions list
                existingAccount.Transactions.ForEach(item => transactions.Add(item.Clone() as Transaction));
                return transactions;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
