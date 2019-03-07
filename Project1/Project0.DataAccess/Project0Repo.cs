using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NLog;
using Project1.BLL;
using P1B = Project1.BLL;

namespace Project0.DataAccess
{
    public class Project0Repo : IProject1Repo
    {
        public static Project0Context Context { get; set; }

        public Project0Repo(Project0Context dbContext)
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

        public void AddCustomer(string fName, string lName, int locationId)
        {
            var newCustomer = new Customer
            {
                FirstName = fName,
                LastName = lName,
                DefaultLocation = locationId
            };
            Context.Customer.Add(newCustomer);
            SaveChangesAndCheckException();
        }

        public void AddCupcakeOrder(int locationId, int customerId)
        {
            var newOrder = new CupcakeOrder
            {
                LocationId = locationId,
                CustomerId = customerId,
                OrderTime = DateTime.Now
            };
            Context.CupcakeOrder.Add(newOrder);
            SaveChangesAndCheckException();
        }

        public void AddCupcakeOrderItems(int orderId, Dictionary<int, int> cupcakeInputs)
        {
            foreach (var cupcake in cupcakeInputs)
            {
                var newOrderItem = new CupcakeOrderItem
                {
                    OrderId = orderId,
                    CupcakeId = cupcake.Key,
                    Quantity = cupcake.Value
                };
                Context.CupcakeOrderItem.Add(newOrderItem);
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

        public int GetLastCupcakeOrderAdded()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.CupcakeOrder.OrderByDescending(x => x.OrderId).First().OrderId;
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return -1;
            }
        }

        public int GetDefaultLocation(int customerId)
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

        public P1B.Order GetCupcakeOrder(int orderId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.CupcakeOrder.Single(co => co.OrderId == orderId));
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public P1B.Cupcake GetCupcake(int cupcakeId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Cupcake.Single(c => c.CupcakeId == cupcakeId));
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public Dictionary<int, Dictionary<int, decimal>> GetRecipes(Dictionary<int, int> cupcakeInputs)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>();

                // Get each recipe for each cupcake that is in the order
                foreach (var item in cupcakeInputs)
                {
                    Dictionary<int, decimal> recipe = new Dictionary<int, decimal>();
                    foreach (var recipeItem in Context.RecipeItem.Where(r => r.CupcakeId == item.Key).ToList())
                    {
                        recipe[recipeItem.IngredientId] = recipeItem.Amount;
                    }
                    recipes[item.Key] = recipe;
                }

                return recipes;
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

        public IEnumerable<P1B.Customer> GetAllCustomers()
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

        public IEnumerable<P1B.Cupcake> GetAllCupcakes()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.Cupcake.ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<P1B.Order> GetAllOrders()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.CupcakeOrder.ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<P1B.OrderItem> GetAllOrderItems()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.CupcakeOrderItem.ToList());
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

        public IEnumerable<P1B.Order> GetCustomerOrderHistory(int customerId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.CupcakeOrder.Where(co => co.CustomerId == customerId).ToList());
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<P1B.OrderItem> GetOrderItems(int orderId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Mapper.Map(Context.CupcakeOrderItem.Where(coi => coi.OrderId == orderId));
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public IEnumerable<P1B.OrderItem> GetCustomerOrderItems(int customerId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                var customerOrders = Context.CupcakeOrder.Where(co => co.CustomerId == customerId)
                .Select(co => co.OrderId)
                .ToList();
                return Mapper.Map(Context.CupcakeOrderItem.Where(coi => customerOrders.Contains(coi.OrderId)));
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

        public bool CheckCupcakeExists(int cupcakeId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.Cupcake.Any(l => l.CupcakeId == cupcakeId);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public bool CheckOrderExists(int orderId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            try
            {
                return Context.CupcakeOrder.Any(l => l.OrderId == orderId);
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
