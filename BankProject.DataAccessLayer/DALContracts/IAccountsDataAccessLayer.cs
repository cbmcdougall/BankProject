using BankProject.Entities;
using System;
using System.Collections.Generic;

namespace BankProject.DataAccessLayer.DALContracts
{
    /// <summary>
    /// Interface representing the accounts data access layer
    /// </summary>
    public interface IAccountsDataAccessLayer
    {
        /// <summary>
        /// Returns all existing accounts
        /// </summary>
        /// <returns>The list of all account objects</returns>
        List<Account> GetAccounts();

        /// <summary>
        /// Returns a set of accounts matching a specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression containing the condition to check</param>
        /// <returns>The list of matching account objects</returns>
        List<Account> GetAccountsByCondition(Predicate<Account> predicate);

        /// <summary>
        /// Adds a new account to the existing accounts list
        /// </summary>
        /// <param name="account">The account object to add</param>
        /// <returns>The Guid of the successfully added account</returns>
        Guid AddAccount(Account account);

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="account">A Account object containing account details to update</param>
        /// <returns>True if the update is successful</returns>
        bool UpdateAccount(Account account);

        /// <summary>
        /// Deletes an existing account by their unique AccountID
        /// </summary>
        /// <param name="accountID">The Guid of the account to delete</param>
        /// <returns>True if the account is successfully deleted</returns>
        bool DeleteAccount(Guid accountID);

        /// <summary>
        /// Adds a transaction to the specified account
        /// </summary>
        /// <param name="transaction">The transaction object to add</param>
        /// <param name="account">The account to add the transaction to</param>
        /// <returns>The Guid of the transaction</returns>
        Guid AddTransaction(Transaction transaction, Account account);

        /// <summary>
        /// Returns a list of all transactions on the specified account
        /// </summary>
        /// <param name="account">The account to retrieve transactions for</param>
        /// <returns>The list of all transaction objects</returns>
        List<Transaction> GetTransactions(Account account);
    }
}
