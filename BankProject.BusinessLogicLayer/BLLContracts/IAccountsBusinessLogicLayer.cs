using BankProject.Entities;
using System;
using System.Collections.Generic;

namespace BankProject.BusinessLogicLayer.BLLContracts
{
    /// <summary>
    /// Interface representing the business logic for accounts
    /// </summary>
    public interface IAccountsBusinessLogicLayer
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
        /// <param name="customerCode">The customer code to associate with the new account</param>
        /// <returns>The Guid of the successfully added account</returns>
        Guid AddAccount(long customerCode);

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
        /// Handles a transaction of funds between two specified accounts
        /// </summary>
        /// <param name="sourceAccount">The account to transfer funds from</param>
        /// <param name="destAccount">The account to transfer funds to</param>
        /// <param name="amount">The amount to transfer</param>
        /// <returns>A boolean indicating if the transfer is a success</returns>
        bool FundTransfer(Account sourceAccount, Account destAccount, double amount);

        /// <summary>
        /// Returns a list of all transactions on the specified account
        /// </summary>
        /// <param name="account">The account to retrieve transactions for</param>
        /// <returns>The list of all transaction objects</returns>
        List<Transaction> GetTransactions(Account account);
    }
}
