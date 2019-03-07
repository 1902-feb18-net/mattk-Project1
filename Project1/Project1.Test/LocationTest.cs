using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Test
{
    public class LocationTest
    {
    //    [Fact]
    //    public void TestCheckCanOrderCupcakeTrue()
    //    {
    //        // Arrange
    //        int locationId = 1;

    //        Dictionary<int, int> cupcakeInputs = new Dictionary<int, int>
    //        {
    //            { 1, 6 },
    //            { 4, 80 },
    //            { 7, 75 }
    //        };

    //        List<Library.Order> orders = new List<Library.Order>
    //        {
    //            new Library.Order
    //            {
    //                Id = 1,
    //                OrderLocation = 1,
    //                OrderCustomer = 1,
    //                OrderTime = DateTime.Now
    //            }
    //        };

    //        List<Library.OrderItem> orderItems = new List<Library.OrderItem>
    //        {
    //            new Library.OrderItem
    //            {
    //                Id = 1,
    //                OrderId = 1,
    //                CupcakeId = 1,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 2,
    //                OrderId = 1,
    //                CupcakeId = 4,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 3,
    //                OrderId = 1,
    //                CupcakeId = 7,
    //                Quantity = 400
    //            },
    //        };

    //        // Act and Assert
    //        Assert.True(Library.Location.CheckCanOrderCupcake(locationId, cupcakeInputs,
    //            orders, orderItems));
    //    }

    //    [Fact]
    //    public void TestCheckCanOrderCupcakeFalse()
    //    {
    //        // Arrange
    //        int locationId = 1;

    //        Dictionary<int, int> cupcakeInputs = new Dictionary<int, int>
    //        {
    //            { 1, 6 },
    //            { 4, 80 },
    //            { 7, 75 }
    //        };

    //        List<Library.Order> orders = new List<Library.Order>
    //        {
    //            new Library.Order
    //            {
    //                Id = 1,
    //                OrderLocation = 1,
    //                OrderCustomer = 1,
    //                OrderTime = DateTime.Now
    //            },
    //            new Library.Order
    //            {
    //                Id = 2,
    //                OrderLocation = 1,
    //                OrderCustomer = 2,
    //                OrderTime = DateTime.Now
    //            },
    //            new Library.Order
    //            {
    //                Id = 3,
    //                OrderLocation = 1,
    //                OrderCustomer = 3,
    //                OrderTime = DateTime.Now
    //            }
    //        };

    //        List<Library.OrderItem> orderItems = new List<Library.OrderItem>
    //        {
    //            new Library.OrderItem
    //            {
    //                Id = 1,
    //                OrderId = 1,
    //                CupcakeId = 1,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 2,
    //                OrderId = 1,
    //                CupcakeId = 4,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 3,
    //                OrderId = 1,
    //                CupcakeId = 7,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 4,
    //                OrderId = 1,
    //                CupcakeId = 1,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 5,
    //                OrderId = 1,
    //                CupcakeId = 4,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 6,
    //                OrderId = 1,
    //                CupcakeId = 7,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 7,
    //                OrderId = 1,
    //                CupcakeId = 1,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 8,
    //                OrderId = 1,
    //                CupcakeId = 4,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 9,
    //                OrderId = 1,
    //                CupcakeId = 7,
    //                Quantity = 400
    //            },
    //        };

    //        // Act and Assert
    //        Assert.False(Library.Location.CheckCanOrderCupcake(locationId, cupcakeInputs,
    //            orders, orderItems));
    //    }

    //    [Fact]
    //    public void TestCheckCanOrderCupcakeTrue24HoursLater()
    //    {
    //        // Arrange
    //        int locationId = 1;

    //        Dictionary<int, int> cupcakeInputs = new Dictionary<int, int>
    //        {
    //            { 1, 6 },
    //            { 4, 80 },
    //            { 7, 75 }
    //        };

    //        List<Library.Order> orders = new List<Library.Order>
    //        {
    //            new Library.Order
    //            {
    //                Id = 1,
    //                OrderLocation = 1,
    //                OrderCustomer = 1,
    //                OrderTime = DateTime.Now.AddMinutes(-1441)
    //            },
    //            new Library.Order
    //            {
    //                Id = 2,
    //                OrderLocation = 1,
    //                OrderCustomer = 2,
    //                OrderTime = DateTime.Now.AddMinutes(-1441)
    //            },
    //            new Library.Order
    //            {
    //                Id = 3,
    //                OrderLocation = 1,
    //                OrderCustomer = 3,
    //                OrderTime = DateTime.Now.AddMinutes(-1441)
    //            }
    //        };

    //        List<Library.OrderItem> orderItems = new List<Library.OrderItem>
    //        {
    //            new Library.OrderItem
    //            {
    //                Id = 1,
    //                OrderId = 1,
    //                CupcakeId = 1,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 2,
    //                OrderId = 1,
    //                CupcakeId = 4,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 3,
    //                OrderId = 1,
    //                CupcakeId = 7,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 4,
    //                OrderId = 1,
    //                CupcakeId = 1,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 5,
    //                OrderId = 1,
    //                CupcakeId = 4,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 6,
    //                OrderId = 1,
    //                CupcakeId = 7,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 7,
    //                OrderId = 1,
    //                CupcakeId = 1,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 8,
    //                OrderId = 1,
    //                CupcakeId = 4,
    //                Quantity = 400
    //            },
    //            new Library.OrderItem
    //            {
    //                Id = 9,
    //                OrderId = 1,
    //                CupcakeId = 7,
    //                Quantity = 400
    //            },
    //        };

    //        // Act and Assert
    //        Assert.True(Library.Location.CheckCanOrderCupcake(locationId, cupcakeInputs,
    //            orders, orderItems));
    //    }

    //    [Fact]
    //    public void TestCheckOrderFeasibleInvGreaterTrue()
    //    {
    //        // Arrange
    //        Dictionary<int, int> cupcakeInputs = new Dictionary<int, int>
    //        {
    //            { 1, 1 },
    //            { 4, 1 },
    //            { 7, 1 }
    //        };

    //        Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>
    //        {
    //            {
    //                1,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 1 },
    //                    { 2, 1 },
    //                    { 3, 1 },
    //                    { 4, 1 },
    //                    { 5, 1 },
    //                    { 6, 1 },
    //                    { 7, 1 },
    //                    { 8, 1 },
    //                    { 9, 1 },
    //                    { 10, 1 },
    //                    { 11, 1 },
    //                    { 12, 1 },
    //                    { 13, 1 },
    //                    { 14, 1 },
    //                    { 15, 1 },
    //                    { 16, 1 },
    //                    { 17, 1 },
    //                    { 18, 1 }
    //                }
    //            },
    //            {
    //                4,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 2 },
    //                    { 2, 2 },
    //                    { 3, 2 },
    //                    { 4, 2 },
    //                    { 5, 2 },
    //                    { 6, 2 },
    //                    { 7, 2 },
    //                    { 8, 2 },
    //                    { 9, 2 },
    //                    { 10, 2 },
    //                    { 11, 2 },
    //                    { 12, 2 },
    //                    { 13, 2 },
    //                    { 14, 2 },
    //                    { 15, 2 },
    //                    { 16, 2 },
    //                    { 17, 2 },
    //                    { 18, 2 }
    //                }
    //            },
    //            {
    //                7,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 3 },
    //                    { 2, 3 },
    //                    { 3, 3 },
    //                    { 4, 3 },
    //                    { 5, 3 },
    //                    { 6, 3 },
    //                    { 7, 3 },
    //                    { 8, 3 },
    //                    { 9, 3 },
    //                    { 10, 3 },
    //                    { 11, 3 },
    //                    { 12, 3 },
    //                    { 13, 3 },
    //                    { 14, 3 },
    //                    { 15, 3 },
    //                    { 16, 3 },
    //                    { 17, 3 },
    //                    { 18, 3 }
    //                }
    //            }
    //        };

    //        Dictionary<int, decimal> locationInv = new Dictionary<int, decimal>
    //        {
    //            { 1, 8 },
    //            { 2, 8 },
    //            { 3, 8 },
    //            { 4, 8 },
    //            { 5, 8 },
    //            { 6, 8 },
    //            { 7, 8 },
    //            { 8, 8 },
    //            { 9, 8 },
    //            { 10, 8 },
    //            { 11, 8 },
    //            { 12, 8 },
    //            { 13, 8 },
    //            { 14, 8 },
    //            { 15, 8 },
    //            { 16, 8 },
    //            { 17, 8 },
    //            { 18, 8 }
    //        };

    //        // Act and Assert
    //        Assert.True(Library.Location.CheckOrderFeasible(recipes, locationInv, cupcakeInputs));
    //    }

    //    [Fact]
    //    public void TestCheckOrderFeasibleInvEqualTrue()
    //    {
    //        // Arrange
    //        Dictionary<int, int> cupcakeInputs = new Dictionary<int, int>
    //        {
    //            { 1, 1 },
    //            { 4, 1 },
    //            { 7, 1 }
    //        };

    //        Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>
    //        {
    //            {
    //                1,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 1 },
    //                    { 2, 1 },
    //                    { 3, 1 },
    //                    { 4, 1 },
    //                    { 5, 1 },
    //                    { 6, 1 },
    //                    { 7, 1 },
    //                    { 8, 1 },
    //                    { 9, 1 },
    //                    { 10, 1 },
    //                    { 11, 1 },
    //                    { 12, 1 },
    //                    { 13, 1 },
    //                    { 14, 1 },
    //                    { 15, 1 },
    //                    { 16, 1 },
    //                    { 17, 1 },
    //                    { 18, 1 }
    //                }
    //            },
    //            {
    //                4,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 2 },
    //                    { 2, 2 },
    //                    { 3, 2 },
    //                    { 4, 2 },
    //                    { 5, 2 },
    //                    { 6, 2 },
    //                    { 7, 2 },
    //                    { 8, 2 },
    //                    { 9, 2 },
    //                    { 10, 2 },
    //                    { 11, 2 },
    //                    { 12, 2 },
    //                    { 13, 2 },
    //                    { 14, 2 },
    //                    { 15, 2 },
    //                    { 16, 2 },
    //                    { 17, 2 },
    //                    { 18, 2 }
    //                }
    //            },
    //            {
    //                7,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 3 },
    //                    { 2, 3 },
    //                    { 3, 3 },
    //                    { 4, 3 },
    //                    { 5, 3 },
    //                    { 6, 3 },
    //                    { 7, 3 },
    //                    { 8, 3 },
    //                    { 9, 3 },
    //                    { 10, 3 },
    //                    { 11, 3 },
    //                    { 12, 3 },
    //                    { 13, 3 },
    //                    { 14, 3 },
    //                    { 15, 3 },
    //                    { 16, 3 },
    //                    { 17, 3 },
    //                    { 18, 3 }
    //                }
    //            }
    //        };

    //        Dictionary<int, decimal> locationInv = new Dictionary<int, decimal>
    //        {
    //            { 1, 6 },
    //            { 2, 6 },
    //            { 3, 6 },
    //            { 4, 6 },
    //            { 5, 6 },
    //            { 6, 6 },
    //            { 7, 6 },
    //            { 8, 6 },
    //            { 9, 6 },
    //            { 10, 6 },
    //            { 11, 6 },
    //            { 12, 6 },
    //            { 13, 6 },
    //            { 14, 6 },
    //            { 15, 6 },
    //            { 16, 6 },
    //            { 17, 6 },
    //            { 18, 6 }
    //        };

    //        // Act and Assert
    //        Assert.True(Library.Location.CheckOrderFeasible(recipes, locationInv, cupcakeInputs));
    //    }

    //    [Fact]
    //    public void TestCheckOrderFeasibleInvLessFalse()
    //    {
    //        // Arrange
    //        Dictionary<int, int> cupcakeInputs = new Dictionary<int, int>
    //        {
    //            { 1, 1 },
    //            { 4, 1 },
    //            { 7, 1 }
    //        };

    //        Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>
    //        {
    //            {
    //                1,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 1 },
    //                    { 2, 1 },
    //                    { 3, 1 },
    //                    { 4, 1 },
    //                    { 5, 1 },
    //                    { 6, 1 },
    //                    { 7, 1 },
    //                    { 8, 1 },
    //                    { 9, 1 },
    //                    { 10, 1 },
    //                    { 11, 1 },
    //                    { 12, 1 },
    //                    { 13, 1 },
    //                    { 14, 1 },
    //                    { 15, 1 },
    //                    { 16, 1 },
    //                    { 17, 1 },
    //                    { 18, 1 }
    //                }
    //            },
    //            {
    //                4,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 2 },
    //                    { 2, 2 },
    //                    { 3, 2 },
    //                    { 4, 2 },
    //                    { 5, 2 },
    //                    { 6, 2 },
    //                    { 7, 2 },
    //                    { 8, 2 },
    //                    { 9, 2 },
    //                    { 10, 2 },
    //                    { 11, 2 },
    //                    { 12, 2 },
    //                    { 13, 2 },
    //                    { 14, 2 },
    //                    { 15, 2 },
    //                    { 16, 2 },
    //                    { 17, 2 },
    //                    { 18, 2 }
    //                }
    //            },
    //            {
    //                7,
    //                new Dictionary<int, decimal>
    //                {
    //                    { 1, 3 },
    //                    { 2, 3 },
    //                    { 3, 3 },
    //                    { 4, 3 },
    //                    { 5, 3 },
    //                    { 6, 3 },
    //                    { 7, 3 },
    //                    { 8, 3 },
    //                    { 9, 3 },
    //                    { 10, 3 },
    //                    { 11, 3 },
    //                    { 12, 3 },
    //                    { 13, 3 },
    //                    { 14, 3 },
    //                    { 15, 3 },
    //                    { 16, 3 },
    //                    { 17, 3 },
    //                    { 18, 3 }
    //                }
    //            }
    //        };

    //        Dictionary<int, decimal> locationInv = new Dictionary<int, decimal>
    //        {
    //            { 1, 0 },
    //            { 2, 0 },
    //            { 3, 0 },
    //            { 4, 0 },
    //            { 5, 0 },
    //            { 6, 0 },
    //            { 7, 0 },
    //            { 8, 0 },
    //            { 9, 0 },
    //            { 10, 0 },
    //            { 11, 0 },
    //            { 12, 0 },
    //            { 13, 0 },
    //            { 14, 0 },
    //            { 15, 0 },
    //            { 16, 0 },
    //            { 17, 0 },
    //            { 18, 0 }
    //        };

    //        // Act and Assert
    //        Assert.False(Library.Location.CheckOrderFeasible(recipes, locationInv, cupcakeInputs));
    //    }
    }
}
