using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
     public interface ICustomerRepo
    {
        bool CheckCustomerExists(int customerId);
        void AddCustomer(Project1.BLL.Customer customer);
        int GetLastCustomerAdded();
        IEnumerable<Project1.BLL.Customer> GetAllCustomers();
    }
}
