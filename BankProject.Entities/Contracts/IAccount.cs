using System;
using System.Collections.Generic;

namespace BankProject.Entities.Contracts
{
    /// <summary>
    /// Interface representing the Account entity
    /// </summary>
    public interface IAccount
    {
        #region Properties
        /// <summary>
        /// Guid for the unique identification of an account
        /// </summary>
        Guid AccountID { get; set; }
        /// <summary>
        /// Auto-generated code number of the account
        /// </summary>
        long AccountCode { get; set; }
        /// <summary>
        /// Code number of the customer account-holder
        /// </summary>
        long CustomerCode { get; set; }
        /// <summary>
        /// Name of the customer account-holder
        /// </summary>
        string CustomerName { get; set; }
        /// <summary>
        /// Account balance
        /// </summary>
        double Balance { get; set; }
        /// <summary>
        /// List of transactions on the account
        /// </summary>
        List<Transaction> Transactions { get; set; }
        #endregion
    }
}
