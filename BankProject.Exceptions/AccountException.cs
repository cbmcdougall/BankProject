using System;

namespace BankProject.Exceptions
{
    /// <summary>
    /// Represents exceptions raised in the Accounts class
    /// </summary>
    public class AccountException : ApplicationException
    {
        /// <summary>
        /// Constructor that initialises the exception message
        /// </summary>
        /// <param name="message">The exception message</param>
        public AccountException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor that initialises the exception message with an inner exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public AccountException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
