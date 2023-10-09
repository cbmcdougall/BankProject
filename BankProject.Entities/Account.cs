using BankProject.Entities.Contracts;
using BankProject.Exceptions;
using System;
using System.Collections.Generic;

namespace BankProject.Entities
{
    /// <summary>
    /// Represents an account held by customers of the bank
    /// </summary>
    public class Account : IAccount, ICloneable
    {
        #region Private Fields
        private Guid _accountID;
        private long _accountCode;
        private long _customerCode;
        private string _customerName;
        private double _balance;
        private List<Transaction> _transactions;
        #endregion

        #region Public Properties
        public Guid AccountID { get => _accountID; set => _accountID = value; }
        public long AccountCode
        {
            get => _accountCode;
            set
            {
                if (value > 0)
                {
                    _accountCode = value;
                }
                else
                {
                    throw new AccountException("Account code should be a positive number.");
                }
            }
        }
        public long CustomerCode
        {
            get => _customerCode;
            set
            {
                if (value > 0)
                {
                    _customerCode = value;
                }
                else
                {
                    throw new AccountException("Customer code should be a positive number.");
                }
            }
        }
        public string CustomerName
        {
            get => _customerName;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 40)
                {
                    _customerName = value;
                }
                else
                {
                    throw new AccountException("Customer name should be non-null and less than 40 characters long.");
                }
            }
        }
        public double Balance { get => _balance; set => _balance = value; }
        public List<Transaction> Transactions { get => _transactions; set => _transactions = value; }
        #endregion

        #region Methods
        public object Clone()
        {
            return new Account()
            {
                AccountID = this.AccountID,
                AccountCode = this.AccountCode,
                CustomerCode = this.CustomerCode,
                CustomerName = this.CustomerName,
                Balance = this.Balance,
                Transactions = this.Transactions
            };
        }
        #endregion
    }
}
