using BankProject.BusinessLogicLayer;
using BankProject.BusinessLogicLayer.BLLContracts;
using BankProject.Entities;
using BankProject.Exceptions;
using System;
using System.Collections.Generic;

namespace BankProject.PresentationLayer
{
    /// <summary>
    /// Handles the UI of the Customers presentation layer
    /// </summary>
    static class CustomersPresentation
    {
        #region Helper Methods
        /// <summary>
        /// Handles logging an exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="logType">Log the exception type as well as message</param>
        private static void LogException(Exception exception, bool logType = false)
        {
            if (logType)
            {
                Console.WriteLine($"{exception.GetType().Name}: {exception.Message}");
            }
            else
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Invokes the BLL to retrieve a customer via a customer code entered by the user.
        /// Raises an exception if the customer code is invalid and allows entering 0 to cancel.
        /// </summary>
        /// <returns>The first matching customer (null if cancelled)</returns>
        /// <exception cref="CustomerException"></exception>
        private static Customer GetCustomerFromUser()
        {
            // Get the input from the user
            bool validInput = long.TryParse(Console.ReadLine(), out long customerCode);
            if (validInput && customerCode == 0) { return null; }

            // Create BLL Object
            ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

            // Find the matching account (pick first match)
            List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(customer => customer.CustomerCode == customerCode);
            if (matchingCustomers.Count >= 1)
            {
                return matchingCustomers[0];
            }
            else
            {
                throw new CustomerException("Invalid Customer code");
            }
        }
        #endregion

        #region Customer Menu Methods
        /// <summary>
        /// Handles the UI for adding a customer to the list of customers
        /// </summary>
        internal static void AddCustomer()
        {
            try
            {
                // Create a Customer object
                Customer customer = new Customer();

                // Read all details from the user
                Console.WriteLine("\n****** ADD CUSTOMER ******");
                Console.Write("Customer Name: ");
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Landmark: ");
                customer.Landmark = Console.ReadLine();
                Console.Write("City: ");
                customer.City = Console.ReadLine();
                Console.Write("Country: ");
                customer.Country = Console.ReadLine();
                Console.Write("Mobile Number: ");
                customer.Mobile = Console.ReadLine();

                // Create BLL Object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                // Add the customer
                Guid newGuid = customersBusinessLogicLayer.AddCustomer(customer);

                // Get the customer code that was generated to display to the user
                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(item => item.CustomerID == newGuid);
                if (matchingCustomers.Count >= 1)
                {
                    Console.WriteLine($"Customer {matchingCustomers[0].CustomerCode} Added.");
                }
                else
                {
                    Console.WriteLine("Customer not added.");
                }
            }
            catch (CustomerException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Handles the UI for updating an existing customer
        /// </summary>
        internal static void UpdateCustomer()
        {
            try
            {
                // Get all customers to display to the user
                if (ViewCustomers())
                {
                    // Get the customer to update
                    Console.Write("Enter the Customer Code of the Customer you wish to update (0 to cancel): ");
                    Customer customerToUpdate = GetCustomerFromUser();
                    if (customerToUpdate == null) { return; }

                    // Get the updated details
                    Console.WriteLine("\n****** NEW CUSTOMER DETAILS ******");
                    Console.Write("Customer Name: ");
                    customerToUpdate.CustomerName = Console.ReadLine();
                    Console.Write("Address: ");
                    customerToUpdate.Address = Console.ReadLine();
                    Console.Write("Landmark: ");
                    customerToUpdate.Landmark = Console.ReadLine();
                    Console.Write("City: ");
                    customerToUpdate.City = Console.ReadLine();
                    Console.Write("Country: ");
                    customerToUpdate.Country = Console.ReadLine();
                    Console.Write("Mobile Number: ");
                    customerToUpdate.Mobile = Console.ReadLine();

                    // Create BLL Object
                    ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                    // Update the customer
                    if (customersBusinessLogicLayer.UpdateCustomer(customerToUpdate))
                    {
                        Console.WriteLine("Customer Updated.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update customer.");
                    }
                }
            }
            catch (CustomerException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Handles the UI for deleting a customer
        /// </summary>
        internal static void DeleteCustomer()
        {
            try
            {
                // Get all customers to display to the user
                if (ViewCustomers())
                {
                    // Get the customer to delete
                    Console.Write("Enter the Customer Code of the Customer you wish to delete (0 to cancel): ");
                    Customer customerToDelete = GetCustomerFromUser();
                    if (customerToDelete == null) { return; }

                    // Create BLL Object
                    ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                    if (customersBusinessLogicLayer.DeleteCustomer(customerToDelete.CustomerID))
                    {
                        Console.WriteLine("Customer Deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete customer.");
                    }
                }
            }
            catch (CustomerException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Handles the UI for searching for a customer via their customer code
        /// </summary>
        internal static void SearchCustomers()
        {
            try
            {
                // Create BLL Object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                // Get the customer code to search
                Console.WriteLine("\n****** SEARCH CUSTOMERS ******");

                bool validInput = false;
                long selectedCode;
                do
                {
                    Console.Write("Enter a Customer Code to search for: ");
                    validInput = long.TryParse(Console.ReadLine(), out selectedCode);
                } while (!validInput);

                // Get matching customers
                List<Customer> matchingCustomers = customersBusinessLogicLayer.GetCustomersByCondition(customer => customer.CustomerCode == selectedCode);

                // Display customer info
                if (matchingCustomers.Count >= 1)
                {
                    foreach (Customer customer in matchingCustomers)
                    {
                        Console.WriteLine($"\nCustomer Code: {customer.CustomerCode}");
                        Console.WriteLine($"Customer Name: {customer.CustomerName}");
                        Console.WriteLine($"Address: {customer.Address}");
                        Console.WriteLine($"Landmark: {customer.Landmark}");
                        Console.WriteLine($"City: {customer.City}");
                        Console.WriteLine($"Country: {customer.Country}");
                        Console.WriteLine($"Mobile: {customer.Mobile}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo Customers found.");
                }

            }
            catch (CustomerException ex)
            {
                LogException(ex);
            }
            catch (Exception ex)
            {
                LogException(ex, true);
            }
        }

        /// <summary>
        /// Displays all the current existing customers
        /// </summary>
        /// <returns>A boolean indicating if there are existing customers to display</returns>
        internal static bool ViewCustomers()
        {
            try
            {
                // Create BLL Object
                ICustomersBusinessLogicLayer customersBusinessLogicLayer = new CustomersBusinessLogicLayer();

                // Get all customers
                List<Customer> allCustomers = customersBusinessLogicLayer.GetCustomers();

                if (allCustomers.Count > 0)
                {
                    // Display customer info
                    Console.WriteLine("\n****** ALL CUSTOMERS ******");
                    foreach (Customer customer in allCustomers)
                    {
                        Console.WriteLine($"Customer Code: {customer.CustomerCode}");
                        Console.WriteLine($"Customer Name: {customer.CustomerName}");
                        Console.WriteLine($"Address: {customer.Address}");
                        Console.WriteLine($"Landmark: {customer.Landmark}");
                        Console.WriteLine($"City: {customer.City}");
                        Console.WriteLine($"Country: {customer.Country}");
                        Console.WriteLine($"Mobile: {customer.Mobile}\n");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("\nThere are currently no customers.");
                    return false;
                }
            }
            catch (CustomerException ex)
            {
                LogException(ex);
                return false;
            }
            catch (Exception ex)
            {
                LogException(ex, true);
                return false;
            }
        }
        #endregion
    }
}
