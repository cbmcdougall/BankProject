using System;

namespace BankProject.Entities.Contracts
{
    /// <summary>
    /// Interface representing the Transfer entity
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Guid for the unique identification of a transfer
        /// </summary>
        Guid TransactionID { get; set; }
        /// <summary>
        /// Date of the transaction
        /// </summary>
        DateTime TransactionDate { get; set; }
        /// <summary>
        /// Type of transaction (IN/OUT)
        /// </summary>
        string TransactionType { get; set; }
        /// <summary>
        /// The code number of the account the transfer was made from/to
        /// </summary>
        long TransactionAccount { get; set; }
        /// <summary>
        /// The amount of money transferred
        /// </summary>
        double TransactionAmount { get; set; }
        /// <summary>
        /// The current balance of the source account as a result of this transactioon
        /// </summary>
        double RollingBalance { get; set; }
    }
}
