using MoreLinq;
using Project1.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project0.ConsoleUI
{
    public static class ConsoleDisplay
    {
        public static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("'L': Add a store location to the database.");
            Console.WriteLine("'C': Add a customer to the database.");
            Console.WriteLine("'O': Add an order to the database.");
            Console.WriteLine("'LL': Get a list of available store locations and their Id numbers.");
            Console.WriteLine("'LO': Get a store's order history.");
            Console.WriteLine("'CL': Get a list of available customers and their information.");
            Console.WriteLine("'CS': Search for customers by name.");
            Console.WriteLine("'CO': Get a customer's order history.");
            Console.WriteLine("'OD': Get order details for one order.");
            Console.WriteLine("'OL': Get a list of all orders that have been placed.");
            Console.WriteLine("'OR': Get a customer's recommended order.");
            Console.WriteLine();
            Console.WriteLine("Please type a selection, or type 'q' to quit: ");
        }

        public static void LocationList(IProject1Repo p0Repo)
        {
            Console.WriteLine("List of Available Store Locations:");
            Console.WriteLine();
            var locations = p0Repo.GetAllLocations().ToList();
            foreach (var item in locations)
            {
                Console.WriteLine($"Location Id: {item.Id}");
            }
        }

        public static void CustomerList(IProject1Repo p0Repo)
        {
            Console.WriteLine("List of Customers:");
            Console.WriteLine();
            var customers = p0Repo.GetAllCustomers().ToList();
            foreach (var item in customers)
            {
                Console.WriteLine($"Customer Id: {item.Id}, First Name: {item.FirstName}, " +
                    $"Last Name, {item.LastName}, Default Location Id: {item.DefaultLocation}");
            }
        }

        public static void OrderList(IProject1Repo p0Repo, List<P1B.Order> orders,
            List<P1B.OrderItem> orderItems, List<P1B.Cupcake> cupcakes, List<P1B.Location> locations)
        {
            Console.WriteLine("Please select from the following filters ('N' for no filter)");
            Console.WriteLine("'E': Earliest orders first");
            Console.WriteLine("'L': Latest orders first");
            Console.WriteLine("'C': Cheapest orders first");
            Console.WriteLine("'X': Most expensive orders first");
            Console.WriteLine();
            Console.WriteLine("Please type a selection to see a list of orders: ");
            ConsoleRead.GetMenuInput(out var input);
            List<P1B.Order> modOrders = new List<P1B.Order>();

            if (input == "E")
            {
                foreach (var item in orders.OrderBy(o => o.OrderTime))
                {
                    modOrders.Add(item);
                }
                DisplayOrders(p0Repo, modOrders, orderItems, cupcakes, locations, "List of Orders (earliest to latest):");
            }
            else if (input == "L")
            {
                foreach (var item in orders.OrderByDescending(o => o.OrderTime))
                {
                    modOrders.Add(item);
                }
                DisplayOrders(p0Repo, modOrders, orderItems, cupcakes, locations, "List of Orders (latest to earliest):");
            }
            else if (input == "C")
            {
                foreach (var item in orders.OrderBy(o =>
                o.GetTotalCost(p0Repo.GetOrderItems(o.Id).ToList(), cupcakes)))
                {
                    modOrders.Add(item);
                }
                DisplayOrders(p0Repo, modOrders, orderItems, cupcakes, locations,
                    "List of Orders (cheapest to most expensive):");
            }
            else if (input == "X")
            {
                foreach (var item in orders.OrderByDescending(o =>
                o.GetTotalCost(p0Repo.GetOrderItems(o.Id).ToList(), cupcakes)))
                {
                    modOrders.Add(item);
                }
                DisplayOrders(p0Repo, modOrders, orderItems, cupcakes, locations,
                    "List of Orders (most expensive to cheapest):");
            }
            else
            {
                DisplayOrders(p0Repo, orders, orderItems, cupcakes, locations, "List of Orders:");
            }
        }

        public static void DisplayOrders(IProject1Repo p0Repo, List<P1B.Order> orders,
            List<P1B.OrderItem> orderItems,
            List<P1B.Cupcake> cupcakes, List<P1B.Location> locations, string prompt)
        {
            Console.WriteLine(prompt);
            Console.WriteLine();
            decimal sum = 0;
            decimal avg = 0;
            int incrementer = 1;
            foreach (var item in orders)
            {
                Console.WriteLine($"Order Id: {item.Id}, Location Id: {item.OrderLocation}, " +
                    $"Customer Id, {item.OrderCustomer}, Order Time: {item.OrderTime},");
                List<P1B.OrderItem> thisOrderItems = p0Repo.GetOrderItems(item.Id).ToList();
                foreach (var orderItem in thisOrderItems)
                {
                    Console.WriteLine($"\tOrder Item {incrementer}: " +
                        $"{cupcakes.Single(c => c.Id == orderItem.CupcakeId).Type}, \n" +
                    $"\tQnty {incrementer}: {orderItem.Quantity}");
                    incrementer++;
                    // Add to the sum for order total and order average
                    sum += orderItem.Quantity * cupcakes.Single(c => c.Id == orderItem.CupcakeId).Cost;
                }

                Console.WriteLine($"Order Id {item.Id} total cost: ${sum}");
                avg += sum;
                sum = 0;
                incrementer = 1;
                Console.WriteLine();
            }
            if (orders.Count() > 0)
            {
                avg /= orders.Count();
                // https://stackoverflow.com/questions/1291483/leave-only-two-decimal-places-after-the-dot
                // This takes the decimal average and stringifys it to two decimal places
                string avgString = String.Format("{0:0.00}", avg);
                Console.WriteLine("Other order statistics...");
                Console.WriteLine($"Average Order Total: " +
                    $"${avgString}");
                Console.WriteLine($"Order with the latest date: " +
                    $"{orders.Max(o => o.OrderTime)}");
                if (!(locations is null))
                {
                    var storeWithMostOrders = locations.MaxBy(sL =>
                    p0Repo.GetLocationOrderHistory(sL.Id).Count()).OrderBy(sL => sL.Id).First();
                    Console.WriteLine($"Store Id with the most orders: {storeWithMostOrders.Id}");
                }
            }
        }

        public static void CupcakeList(List<P1B.Cupcake> cupcakes)
        {
            Console.WriteLine("List of Cupcakes (available to add to order):");
            Console.WriteLine();
            foreach (var item in cupcakes)
            {
                Console.WriteLine($"{item.Id}: {item.Type}");
            }

            Console.WriteLine();
        }

        public static void DisplayOrder(P1B.Order order, List<P1B.OrderItem> orderItems,
            List<P1B.Cupcake> cupcakes)
        {
            decimal sum = 0;
            int incrementer = 1;
            Console.WriteLine($"Order Id: {order.Id}, Location Id: {order.OrderLocation}, " +
                    $"Customer Id, {order.OrderCustomer}, Order Time: {order.OrderTime},");
            foreach (var orderItem in orderItems)
            {
                Console.WriteLine($"\tOrder Item {incrementer}: " +
                    $"{cupcakes.Single(c => c.Id == orderItem.CupcakeId).Type}, \n" +
                $"\tQnty {incrementer}: {orderItem.Quantity}");
                incrementer++;
                sum += orderItem.Quantity * cupcakes.Single(c => c.Id == orderItem.CupcakeId).Cost;
            }

            Console.WriteLine($"Order Id {order.Id} total cost: ${sum}");
            Console.WriteLine();
        }
    }
}
