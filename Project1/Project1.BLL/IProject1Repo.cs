using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL
{
    public interface IProject1Repo
    {
        void AddCustomer(string fName, string lName, int locationId);
        void AddCupcakeOrder(int locationId, int customerId);
        void AddCupcakeOrderItems(int orderId, Dictionary<int, int> cupcakeInputs);
        int GetLastCustomerAdded();
        int GetLastCupcakeOrderAdded();
        Project1.BLL.Order GetCupcakeOrder(int orderId);
        Project1.BLL.Cupcake GetCupcake(int cupcakeId);
        Dictionary<int, Dictionary<int, decimal>> GetRecipes(Dictionary<int, int> cupcakeInputs);
        IEnumerable<Project1.BLL.Customer> GetAllCustomers();
        IEnumerable<Project1.BLL.Cupcake> GetAllCupcakes();
        IEnumerable<Project1.BLL.Order> GetAllOrders();
        IEnumerable<Project1.BLL.OrderItem> GetAllOrderItems();
        IEnumerable<Project1.BLL.Order> GetCustomerOrderHistory(int customerId);
        IEnumerable<Project1.BLL.OrderItem> GetOrderItems(int orderId);
        IEnumerable<Project1.BLL.OrderItem> GetCustomerOrderItems(int customerId);
        bool CheckCustomerExists(int customerId);
        bool CheckCupcakeExists(int cupcakeId);
        bool CheckOrderExists(int orderId);
    }
}
