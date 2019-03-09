using System;
using System.Collections.Generic;
using System.Text;
using P1B = Project1.BLL;

namespace Project1.BLL.IDataRepos
{
    public interface ILocationInventoryRepo
    {
        IEnumerable<P1B.LocationInventory> GetLocationInventoryByLocationId(int locationId);
        void FillLocationInventory(int locationId);
        void UpdateLocationInv(int locationId, Dictionary<int, Dictionary<int, decimal>> recipes,
            Dictionary<int, int> cupcakeInputs);
    }
}
