using BankProject.DataAccessLayer.DALContracts;
using BankProject.Entities;
using System;
using System.Collections.Generic;

namespace BankProject.DataAccessLayer
{
    /// <summary>
    /// Represents the DAL for bank customers
    /// </summary>
    public class CustomersDataAccessLayer : ICustomersDataAccessLayer
    {
        #region Fields
        private static List<Customer> _customers;
        #endregion

        #region Constructors
        static CustomersDataAccessLayer()
        {
            _customers = new List<Customer>();
        }
        #endregion

        #region Properties
        private static List<Customer> Customers
        {
            get => _customers;
            set => _customers = value;
        }
        #endregion

        #region Methods
        public List<Customer> GetCustomers()
        {
            try
            {
                // Create a new Customers list
                List<Customer> customersList = new List<Customer>();

                // Copy all customers from the source collection into the new Customers list
                Customers.ForEach(item => customersList.Add(item.Clone() as Customer));
                return customersList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                // Create a new Customers list
                List<Customer> customersList = new List<Customer>();

                // Filter the collection
                List<Customer> filteredCustomers = Customers.FindAll(predicate);

                // Copy all customers from the filtered collection into the new Customers list
                filteredCustomers.ForEach(item => customersList.Add(item.Clone() as Customer));
                return customersList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddCustomer(Customer customer)
        {
            try
            {
                // Generate new Guid
                customer.CustomerID = Guid.NewGuid();

                // Add the customer to the collection
                Customers.Add(customer);
                return customer.CustomerID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                // Find the existing customer by CustomerID
                Customer existingCustomer = Customers.Find(item => item.CustomerID == customer.CustomerID);

                // Update all details of customer except for CustomerID and CustomerCode
                if (existingCustomer != null)
                {
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Landmark = customer.Landmark;
                    existingCustomer.City = customer.City;
                    existingCustomer.Country = customer.Country;
                    existingCustomer.Mobile = customer.Mobile;

                    return true; // Customer is updated
                }
                else
                {
                    return false; // Customer could not be found/updated
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCustomer(Guid customerID)
        {
            try
            {
                return Customers.RemoveAll(item => item.CustomerID == customerID) > 0; // True if Customer was successfully deleted
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}