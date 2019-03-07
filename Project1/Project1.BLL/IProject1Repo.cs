using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL
{
    public interface IProject1Repo
    {
        void AddLocation();
        void FillLocationInventory(int locationId);
        void AddCustomer(string fName, string lName, int locationId);
        void AddCupcakeOrder(int locationId, int customerId);
        void AddCupcakeOrderItems(int orderId, Dictionary<int, int> cupcakeInputs);
        int GetLastLocationAdded();
        int GetLastCustomerAdded();
        int GetLastCupcakeOrderAdded();
        Project1.BLL.Order GetCupcakeOrder(int orderId);
        Project1.BLL.Cupcake GetCupcake(int cupcakeId);
        int GetDefaultLocation(int customerId);
        Dictionary<int, Dictionary<int, decimal>> GetRecipes(Dictionary<int, int> cupcakeInputs);
        Dictionary<int, decimal> GetLocationInv(int locationId);
        IEnumerable<Project1.BLL.Location> GetAllLocations();
        IEnumerable<Project1.BLL.Customer> GetAllCustomers();
        IEnumerable<Project1.BLL.Cupcake> GetAllCupcakes();
        IEnumerable<Project1.BLL.Order> GetAllOrders();
        IEnumerable<Project1.BLL.OrderItem> GetAllOrderItems();
        IEnumerable<Project1.BLL.Order> GetLocationOrderHistory(int locationId);
        IEnumerable<Project1.BLL.Order> GetCustomerOrderHistory(int customerId);
        IEnumerable<Project1.BLL.OrderItem> GetOrderItems(int orderId);
        IEnumerable<Project1.BLL.OrderItem> GetCustomerOrderItems(int customerId);
        bool CheckLocationExists(int locationId);
        bool CheckCustomerExists(int customerId);
        bool CheckCupcakeExists(int cupcakeId);
        bool CheckOrderExists(int orderId);
        void UpdateLocationInv(int locationId, Dictionary<int, Dictionary<int, decimal>> recipes,
            Dictionary<int, int> cupcakeInputs);
    }
}
