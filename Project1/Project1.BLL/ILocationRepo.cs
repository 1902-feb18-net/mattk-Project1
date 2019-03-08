using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL
{
    public interface ILocationRepo
    {
        void SaveChangesAndCheckException();
        void AddLocation();
        void FillLocationInventory(int locationId);
        int GetLastLocationAdded();
        int GetDefaultLocation(int customerId);
        Dictionary<int, decimal> GetLocationInv(int locationId);
        IEnumerable<Project1.BLL.Location> GetAllLocations();
        IEnumerable<Project1.BLL.Order> GetLocationOrderHistory(int locationId);
        bool CheckLocationExists(int locationId);
        void UpdateLocationInv(int locationId, Dictionary<int, Dictionary<int, decimal>> recipes,
            Dictionary<int, int> cupcakeInputs);

    }
}
