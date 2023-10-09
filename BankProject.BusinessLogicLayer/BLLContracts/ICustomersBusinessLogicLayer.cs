using BankProject.Entities;
using System;
using System.Collections.Generic;

namespace BankProject.BusinessLogicLayer.BLLContracts
{
    /// <summary>
    /// Interface representing the business logic for customers
    /// </summary>
    public interface ICustomersBusinessLogicLayer
    {
        /// <summary>
        /// Returns a list of all existing customers
        /// </summary>
        /// <returns>The list of all customers</returns>
        List<Customer> GetCustomers();

        /// <summary>
        /// Returns a set of customers matching a specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression containing the condition to check</param>
        /// <returns>The list of matching customers</returns>
        List<Customer> GetCustomersByCondition(Predicate<Customer> predicate);

        /// <summary>
        /// Adds a new customer to the existing customers list
        /// </summary>
        /// <param name="customer">The customer object to add</param>
        /// <returns>The Guid of the successfully added customer</returns>
        Guid AddCustomer(Customer customer);

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="customer">A Customer object containing customer details to update</param>
        /// <returns>True if the update is successful</returns>
        bool UpdateCustomer(Customer customer);

        /// <summary>
        /// Deletes an existing customer by their unique CustomerID
        /// </summary>
        /// <param name="customerID">The Guid of the customer to delete</param>
        /// <returns>True if the customer is successfully deleted</returns>
        bool DeleteCustomer(Guid customerID);
    }
}
