using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project1.DataAccess
{
    public static class Mapper
    {
        // Location
        public static P1B.Location Map(DataAccess.Location location) => new P1B.Location
        {
            Id = location.LocationId,
            Name = location.Name
        };
        public static DataAccess.Location Map(P1B.Location location) => new DataAccess.Location
        {
            LocationId = location.Id,
            Name = location.Name
        };
        public static IEnumerable<P1B.Location> Map(IEnumerable<DataAccess.Location> locations) =>
            locations.Select(Map);
        public static IEnumerable<DataAccess.Location> Map(IEnumerable<P1B.Location> locations) =>
            locations.Select(Map);

        // Customer
        public static P1B.Customer Map(DataAccess.Customer customer) => new P1B.Customer
        {
            Id = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DefaultLocation = customer.DefaultLocation
        };
        public static DataAccess.Customer Map(P1B.Customer customer) => new DataAccess.Customer
        {
            CustomerId = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DefaultLocation = customer.DefaultLocation
        };
        public static IEnumerable<P1B.Customer> Map(IEnumerable<DataAccess.Customer> customers) =>
            customers.Select(Map);
        public static IEnumerable<DataAccess.Customer> Map(IEnumerable<P1B.Customer> customers) =>
            customers.Select(Map);

        // Cupcake
        public static P1B.Cupcake Map(DataAccess.Cupcake cupcake) => new P1B.Cupcake
        {
            Id = cupcake.CupcakeId,
            Type = cupcake.Type,
            Cost = cupcake.Cost
        };
        public static DataAccess.Cupcake Map(P1B.Cupcake cupcake) => new DataAccess.Cupcake
        {
            CupcakeId = cupcake.Id,
            Type = cupcake.Type,
            Cost = cupcake.Cost
        };
        public static IEnumerable<P1B.Cupcake> Map(IEnumerable<DataAccess.Cupcake> cupcakes) =>
            cupcakes.Select(Map);
        public static IEnumerable<DataAccess.Cupcake> Map(IEnumerable<P1B.Cupcake> cupcakes) =>
            cupcakes.Select(Map);

        // CupcakeOrder
        public static P1B.Order Map(DataAccess.CupcakeOrder order) => new P1B.Order
        {
            Id = order.OrderId,
            OrderLocation = order.LocationId,
            OrderCustomer = order.CustomerId,
            OrderTime = order.OrderTime
        };
        public static DataAccess.CupcakeOrder Map(P1B.Order order) => new DataAccess.CupcakeOrder
        {
            OrderId = order.Id,
            LocationId = order.OrderLocation,
            CustomerId = order.OrderCustomer,
            OrderTime = order.OrderTime
        };
        public static IEnumerable<P1B.Order> Map(IEnumerable<DataAccess.CupcakeOrder> orders) =>
            orders.Select(Map);
        public static IEnumerable<DataAccess.CupcakeOrder> Map(IEnumerable<P1B.Order> orders) =>
            orders.Select(Map);

        // CupcakeOrderItem
        public static P1B.OrderItem Map(DataAccess.CupcakeOrderItem orderItem) => new P1B.OrderItem
        {
            Id = orderItem.CupcakeOrderItemId,
            OrderId = orderItem.OrderId,
            CupcakeId = orderItem.CupcakeId,
            Quantity = orderItem.Quantity
        };
        public static DataAccess.CupcakeOrderItem Map(P1B.OrderItem orderItem) => new DataAccess.CupcakeOrderItem
        {
            CupcakeOrderItemId = orderItem.Id,
            OrderId = orderItem.OrderId,
            CupcakeId = orderItem.CupcakeId,
            Quantity = orderItem.Quantity
        };
        public static IEnumerable<P1B.OrderItem> Map(IEnumerable<DataAccess.CupcakeOrderItem> orderItems) =>
            orderItems.Select(Map);
        public static IEnumerable<DataAccess.CupcakeOrderItem> Map(IEnumerable<P1B.OrderItem> orderItems) =>
            orderItems.Select(Map);
    }
}
