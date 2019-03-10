using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Project1.BLL;
using Project1.BLL.IDataRepos;

namespace Project1.DataAccess.DataRepos
{
    public class CustomerRepo : IProject1Repo, ICustomerRepo
    {
        public readonly ILogger<CustomerRepo> _logger;

        public static Project1Context Context { get; set; }

        public CustomerRepo(Project1Context dbContext)
        {
            Context = dbContext;
        }

        public void SaveChangesAndCheckException()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public bool CheckCustomerExists(int customerId)
        {
            try
            {
                return Context.Customer.Any(l => l.CustomerId == customerId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public void AddCustomer(Project1.BLL.Customer customer)
        {
            var newCustomer = new Project1.DataAccess.DataClasses.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DefaultLocation = customer.DefaultLocation
            };
            Context.Customer.Add(newCustomer);
            SaveChangesAndCheckException();
        }

        public int GetLastCustomerAdded()
        {
            try
            {
                return Context.Customer.OrderByDescending(x => x.CustomerId).First().CustomerId;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return -1;
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                return Mapper.Map(Context.Customer.ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<Order> GetCustomerOrderHistory(int customerId)
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrder.Where(co => co.CustomerId == customerId).ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<OrderItem> GetCustomerOrderItems(int customerId)
        {
            try
            {
                var customerOrders = Context.CupcakeOrder.Where(co => co.CustomerId == customerId)
                .Select(co => co.OrderId)
                .ToList();
                return Mapper.Map(Context.CupcakeOrderItem.Where(coi => customerOrders.Contains(coi.OrderId)));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
