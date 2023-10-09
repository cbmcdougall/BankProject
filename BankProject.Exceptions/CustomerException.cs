using System;

namespace BankProject.Exceptions
{
    /// <summary>
    /// Represents exceptions raised in the Customer class
    /// </summary>
    public class CustomerException : ApplicationException
    {
        /// <summary>
        /// Constructor that initialises the exception message
        /// </summary>
        /// <param name="message">The exception message</param>
        public CustomerException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor that initialises the exception message with an inner exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public CustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
