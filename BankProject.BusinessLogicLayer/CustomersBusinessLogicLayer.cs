using BankProject.BusinessLogicLayer.BLLContracts;
using BankProject.Configuration;
using BankProject.DataAccessLayer;
using BankProject.DataAccessLayer.DALContracts;
using BankProject.Entities;
using System;
using System.Collections.Generic;

namespace BankProject.BusinessLogicLayer
{
    /// <summary>
    /// Represents customer business logic
    /// </summary>
    public class CustomersBusinessLogicLayer : ICustomersBusinessLogicLayer
    {
        #region Private Fields
        private ICustomersDataAccessLayer _customersDataAccessLayer;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that initialises the data access layer
        /// </summary>
        public CustomersBusinessLogicLayer()
        {
            _customersDataAccessLayer = new CustomersDataAccessLayer();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Private property to represent a reference of CustomersDataAccessLayer
        /// </summary>
        private ICustomersDataAccessLayer CustomersDataAccessLayer
        {
            get => _customersDataAccessLayer;
            set => _customersDataAccessLayer = value;
        }
        #endregion

        #region Methods
        public List<Customer> GetCustomers()
        {
            try
            {
                // Invoke the DAL to get customers
                return CustomersDataAccessLayer.GetCustomers();
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
                // Invoke the DAL to get customers
                return CustomersDataAccessLayer.GetCustomersByCondition(predicate);
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
                // Get all customers
                List<Customer> allCustomers = CustomersDataAccessLayer.GetCustomers();

                // Get the largest customer code of all customers, defaulting to the configured base customer code
                long maxCustCode = Settings.BaseCustomerNo;
                allCustomers.ForEach(item => maxCustCode = (item.CustomerCode > maxCustCode) ? item.CustomerCode : maxCustCode);

                // Generate new customer number
                customer.CustomerCode = maxCustCode + 1;

                // Invoke the DAL to add the customer
                return CustomersDataAccessLayer.AddCustomer(customer);
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
                // Invoke the DAL to update the customer
                return CustomersDataAccessLayer.UpdateCustomer(customer);
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
                // Invoke the DAL to delete the customer
                return CustomersDataAccessLayer.DeleteCustomer(customerID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
