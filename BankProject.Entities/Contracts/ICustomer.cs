using System;

namespace BankProject.Entities.Contracts
{
    /// <summary>
    /// Interface representing the customer entity
    /// </summary>
    public interface ICustomer
    {
        #region Properties
        /// <summary>
        /// Guid for the unique identification of a customer
        /// </summary>
        Guid CustomerID { get; set; }
        /// <summary>
        /// Auto-generated code number of the customer
        /// </summary>
        long CustomerCode { get; set; }
        /// <summary>
        /// Name of the customer
        /// </summary>
        string CustomerName { get; set; }
        /// <summary>
        /// Address of the customer
        /// </summary>
        string Address { get; set; }
        /// <summary>
        /// Landmark of the customer's address
        /// </summary>
        string Landmark { get; set; }
        /// <summary>
        /// City of the customer
        /// </summary>
        string City { get; set; }
        /// <summary>
        /// Country of the customer
        /// </summary>
        string Country { get; set; }
        /// <summary>
        /// 10-digit Mobile number of the customer
        /// </summary>
        string Mobile { get; set; }
        #endregion
    }
}
