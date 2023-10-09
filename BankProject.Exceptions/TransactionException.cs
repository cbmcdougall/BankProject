using System;

namespace BankProject.Exceptions
{
    /// <summary>
    /// Represents exceptions raised in the Transaction class
    /// </summary>
    public class TransactionException : ApplicationException
    {
        /// <summary>
        /// Constructor that initialises the exception message
        /// </summary>
        /// <param name="message">The exception message</param>
        public TransactionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor that initialises the exception message with an inner exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public TransactionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
