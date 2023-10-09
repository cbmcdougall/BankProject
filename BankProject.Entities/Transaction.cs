using BankProject.Entities.Contracts;
using BankProject.Exceptions;
using System;

namespace BankProject.Entities
{
    public class Transaction : ITransaction, ICloneable
    {
        #region Private Fields
        private Guid _transactionID;
        private DateTime _transactionDate;
        private string _transactionType;
        private long _transactionAccount;
        private double _transactionAmount;
        private double _rollingBalance;
        #endregion

        #region Public Properties
        public Guid TransactionID { get => _transactionID; set => _transactionID = value; }
        public DateTime TransactionDate { get => _transactionDate; set => _transactionDate = value; }
        public string TransactionType
        {
            get => _transactionType;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || (value != "IN" && value != "OUT"))
                {
                    throw new TransactionException("Transaction type must be \"IN\" or \"OUT\".");
                }
                else
                {
                    _transactionType = value;
                }
            }
        }
        public long TransactionAccount
        {
            get => _transactionAccount;
            set
            {
                if (value > 0)
                {
                    _transactionAccount = value;
                }
                else
                {
                    throw new AccountException("Account code should be a positive number.");
                }
            }
        }
        public double TransactionAmount
        {
            get => _transactionAmount;
            set
            {
                if (value > 0)
                {
                    _transactionAmount = value;
                }
                else
                {
                    throw new TransactionException("Cannot transfer a negative or zero amount.");
                }
            }
        }
        public double RollingBalance { get => _rollingBalance; set => _rollingBalance = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates and returns a formatted string representing the table headers for an account statement
        /// </summary>
        /// <returns>The formatted statement header string</returns>
        public static string StatementHeader() => $"{"Transaction Date",-18} {"Account",8} {"Money In",10} {"Money Out",10} {"Balance",10}";

        /// <summary>
        /// Creates and returns a formatted string representing the transaction
        /// </summary>
        /// <returns>The formatted transaction string</returns>
        public override string ToString() => $"{this.TransactionDate,-18:g} {this.TransactionAccount,8} {(this.TransactionType == "IN" ? $"{this.TransactionAmount,10:C} {" ",10}" : $"{" ",10} {this.TransactionAmount,10:C}")} {this.RollingBalance,10:C}";

        public object Clone()
        {
            return new Transaction()
            {
                TransactionID = this.TransactionID,
                TransactionDate = this.TransactionDate,
                TransactionType = this.TransactionType,
                TransactionAccount = this.TransactionAccount,
                TransactionAmount = this.TransactionAmount,
                RollingBalance = this.RollingBalance
            };
        }
        #endregion
    }
}
