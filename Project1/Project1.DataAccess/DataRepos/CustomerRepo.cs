using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NLog;
using Project1.BLL;
using Project1.BLL.IDataRepos;

namespace Project1.DataAccess.DataRepos
{
    public class CustomerRepo : IProject1Repo, ICustomerRepo
    {
        public static Project1Context Context { get; set; }

        public CustomerRepo(Project1Context dbContext)
        {
            Context = dbContext;
        }

        public void SaveChangesAndCheckException()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                Context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                logger.Error(ex);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
            }
        }

        public bool CheckCustomerExists(int customerId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Customer.Any(l => l.CustomerId == customerId);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
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
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Customer.OrderByDescending(x => x.CustomerId).First().CustomerId;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return -1;
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Customer.ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }
    }
}
