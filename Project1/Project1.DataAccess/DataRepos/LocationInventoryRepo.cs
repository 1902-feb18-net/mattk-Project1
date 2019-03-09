using Microsoft.EntityFrameworkCore;
using NLog;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project1.DataAccess.DataRepos
{
    public class LocationInventoryRepo : IProject1Repo, ILocationInventoryRepo
    {
        public static Project1Context Context { get; set; }

        public LocationInventoryRepo(Project1Context dbContext)
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

        public IEnumerable<P1B.LocationInventory> 
            GetLocationInventoryByLocationId(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.LocationInventory.Include(li => li.Location)
                    .Include(li => li.Ingredient)
                    .Where(li => li.LocationId == locationId));
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public void FillLocationInventory(int locationId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                foreach (var item in Context.Ingredient.ToList())
                {
                    var locationInv = new Project1.DataAccess.DataClasses.LocationInventory();
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
