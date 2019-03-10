using NLog;
using Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class OrderRepo : IProject1Repo, IOrderRepo
    {
        public static Project1Context Context { get; set; }

        public OrderRepo(Project1Context dbContext)
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

        public void AddCupcakeOrder(Project1.BLL.Order order)
        {
            var newOrder = new Project1.DataAccess.DataClasses.CupcakeOrder
            {
                LocationId = order.OrderLocation,
                CustomerId = order.OrderCustomer,
                OrderTime = order.OrderTime
            };
            Context.CupcakeOrder.Add(newOrder);
            SaveChangesAndCheckException();
        }

        public void AddCupcakeOrderItems(int orderId, Dictionary<int, int> cupcakeInputs)
        {
            foreach (var cupcake in cupcakeInputs)
            {
                var newOrderItem = new Project1.DataAccess.DataClasses.CupcakeOrderItem
                {
                    OrderId = orderId,
                    CupcakeId = cupcake.Key,
                    Quantity = cupcake.Value
                };
                Context.CupcakeOrderItem.Add(newOrderItem);
            }
            SaveChangesAndCheckException();
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

        public Order GetOrder(int orderId)
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

        public IEnumerable<Order> GetAllOrders()
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

        public IEnumerable<OrderItem> GetAllOrderItems()
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

        public IEnumerable<OrderItem> GetOrderItems(int orderId)
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
    }
}
