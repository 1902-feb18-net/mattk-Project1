using NLog;
using Project1.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project1.DataAccess
{
    public class LocationRepo : ILocationRepo
    {
        public static Project1Context Context { get; set; }

        public LocationRepo(Project1Context dbContext)
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

        public void AddLocation()
        {
            Context.Location.Add(new Location());
            SaveChangesAndCheckException();
        }

        public void FillLocationInventory(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                foreach (var item in Context.Ingredient.ToList())
                {
                    var locationInv = new LocationInventory();
                    locationInv.IngredientId = item.IngredientId;
                    locationInv.LocationId = locationId;
                    locationInv.Amount = 120;
                    Context.LocationInventory.Add(locationInv);
                }
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
            }

            SaveChangesAndCheckException();
        }

        public int GetLastLocationAdded()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Location.OrderByDescending(x => x.LocationId).First().LocationId;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return -1;
            }
        }

        public int? GetDefaultLocation(int customerId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Customer.Single(c => c.CustomerId == customerId).DefaultLocation;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return -1;
            }
        }

        public IEnumerable<Project1.BLL.Location> GetAllLocations()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Location.ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public Dictionary<int, decimal> GetLocationInv(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                Dictionary<int, decimal> locationInv = new Dictionary<int, decimal>();
                foreach (var item in Context.LocationInventory.Where(li => li.LocationId == locationId))
                {
                    locationInv[item.IngredientId] = item.Amount;
                }
                return locationInv;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<P1B.Order> GetLocationOrderHistory(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.CupcakeOrder.Where(co => co.LocationId == locationId).ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public bool CheckLocationExists(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Location.Any(l => l.LocationId == locationId);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public void UpdateLocationInv(int locationId, Dictionary<int, Dictionary<int, decimal>> recipes,
            Dictionary<int, int> cupcakeInputs)
        {
            // For each cupcake in the order, take that cupcake recipe and cupcake qnty, and subtract
            // the order ingredient amounts required from the store location's inventory.
            // The store location should already have been checked to make sure that its inventory
            // will not go negative from the order.

            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                foreach (var locationInv in Context.LocationInventory.Where(li => li.LocationId == locationId))
                {
                    foreach (var cupcake in cupcakeInputs)
                    {
                        locationInv.Amount -= recipes[cupcake.Key][locationInv.IngredientId] * cupcake.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
            }
            SaveChangesAndCheckException();
        }

    }
}
