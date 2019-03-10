using NLog;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class OrderItemRepo : IProject1Repo, IOrderItemRepo
    {
        public static Project1Context Context { get; set; }

        public OrderItemRepo(Project1Context dbContext)
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

        public void AddCupcakeOrderItems(List<Project1.BLL.OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                Context.CupcakeOrderItem.Add(Mapper.Map(orderItem));
            }
            SaveChangesAndCheckException();
        }

        public IEnumerable<Project1.BLL.OrderItem> GetAllOrderItems()
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

        public IEnumerable<Project1.BLL.OrderItem> GetOrderItems(int orderId)
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
