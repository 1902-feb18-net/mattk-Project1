using Microsoft.Extensions.Logging;
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
        public readonly ILogger<OrderItemRepo> _logger;

        public static Project1Context Context { get; set; }

        public OrderItemRepo(Project1Context dbContext)
        {
            Context = dbContext;
        }

        public void SaveChangesAndCheckException()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
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
            try
            {
                return Mapper.Map(Context.CupcakeOrderItem.ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<Project1.BLL.OrderItem> GetOrderItems(int orderId)
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrderItem.Where(coi => coi.OrderId == orderId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
