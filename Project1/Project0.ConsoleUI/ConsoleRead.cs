using NLog;
using Project1.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project0.ConsoleUI
{
    public static class ConsoleRead
    {
        public static void GetMenuInput(out string input)
        {
            input = Console.ReadLine().ToUpper();
            Console.WriteLine();
        }

        public static void LocationOrders(IProject1Repo p0Repo)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            // Get location 
            int locationId = GetLocation(p0Repo,
                "Please enter the store location Id to get that location's orders:", -1);
            if (locationId == -1)
            {
                return;
            }
            if (!p0Repo.CheckLocationExists(locationId))
            {
                logger.Error("This location is not in the system.");
                return;
            }

            // Get all cupcakes 
            var cupcakes = p0Repo.GetAllCupcakes().ToList();
            // Get specific location orders
            var locationOrderHistory = p0Repo.GetLocationOrderHistory(locationId).ToList();
            // Get all order items
            var orderItems = p0Repo.GetAllOrderItems().ToList();
            Console.WriteLine($"Store Location {locationId}");
            Console.WriteLine();
            // Send all information to OrderList to display the orders
            ConsoleDisplay.OrderList(p0Repo, locationOrderHistory, orderItems, cupcakes, null);
        }

        public static void CustomerSearch(IProject1Repo p0Repo)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            string fName = GetCustomerFirstName();
            if (fName is null) { return; }

            List<P1B.Customer> customers = p0Repo.GetAllCustomers().ToList();

            // Try to find a match to the first name
            var numPossibleMatches = customers.Count(c => c.FirstName == fName);
            if (numPossibleMatches > 0)
            {
                Console.WriteLine($"Found {numPossibleMatches} with that first name.");
                var possibleMatches = customers.Where(c => c.FirstName == fName);
                List<P1B.Customer> customerList = new List<P1B.Customer>();
                foreach (var item in possibleMatches)
                {
                    customerList.Add(item);
                }

                string lName = GetCustomerLastName();
                if (lName is null) { return; }
                // Try to find a match to the last name
                numPossibleMatches = customerList.Count(c => c.LastName == lName);
                if (numPossibleMatches > 0)
                {
                    possibleMatches = customerList.Where(c => c.LastName == lName);
                    Console.WriteLine();
                    Console.WriteLine("List of customer's with that first name and last name:");
                    Console.WriteLine();
                    foreach (var item in possibleMatches)
                    {
                        Console.WriteLine($"Customer Id: {item.Id}, First Name: {item.FirstName}, " +
                        $"Last Name, {item.LastName}, Default Location Id: {item.DefaultLocation}");
                    }
                }
                else
                {
                    logger.Error("There is no one in the system with that first name and last name.");
                }
            }
            else
            {
                logger.Error("There is no one in the system with that first name.");
            }
        }

        public static void CustomerOrders(IProject1Repo p0Repo)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            // Get all customers
            var customers = p0Repo.GetAllCustomers().ToList();

            ConsoleDisplay.CustomerList(p0Repo);
            Console.WriteLine();
            Console.WriteLine("Please enter the customer Id to get that customer's orders:");
            var input = Console.ReadLine();

            if (int.TryParse(input, out var customerId))
            {
                if (!p0Repo.CheckCustomerExists(customerId))
                {
                    logger.Error($"Customer {customerId} is not in the list of customers.");
                    return;
                }
                foreach (var item in customers.Where(c => c.Id == customerId))
                {
                    Console.WriteLine($"Customer {item.FirstName} {item.LastName}");
                    Console.WriteLine();
                    // Get specific customer's orders
                    var customerOrderHistory = p0Repo.GetCustomerOrderHistory(customerId).ToList();
                    // Get all cupcakes
                    var cupcakes = p0Repo.GetAllCupcakes().ToList();
                    // Get all order items
                    var orderItems = p0Repo.GetAllOrderItems().ToList();
                    // Send information to OrderList to be displayed
                    ConsoleDisplay.OrderList(p0Repo, customerOrderHistory, orderItems, cupcakes, null);
                }
            }
            else
            {
                logger.Error($"Invalid input {input}");
            }
        }

        public static void OrderRecommended(IProject1Repo p0Repo)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            int customerId = GetCustomer(p0Repo);
            if (!p0Repo.CheckCustomerExists(customerId)) { return; }

            var customer = p0Repo.GetAllCustomers().ToList().Single(c => c.Id == customerId);
            var orderItems = p0Repo.GetAllOrderItems().ToList();
            var cupcakes = p0Repo.GetAllCupcakes().ToList();

            var customerOrderItems = p0Repo.GetCustomerOrderItems(customerId).ToList();
            if (customerOrderItems.Count() > 0)
            {
                // https://stackoverflow.com/questions/6730974/select-most-frequent-value-using-linq
                // Get the most frequent order, if there is a tie, then lowest Id wins
                var mostFrequentOrder = customerOrderItems.OrderBy(o => o.CupcakeId)
                                                        .GroupBy(o => o.CupcakeId)
                                                        .OrderByDescending(gp => gp.Count())
                                                        .Take(1);
                // https://code.i-harness.com/en/q/820541
                var intermediate = mostFrequentOrder.First();
                string orderRecommended = "not assigned";
                foreach (var item in intermediate)
                {
                    orderRecommended = cupcakes.Single(c => c.Id == item.CupcakeId).Type;
                    break;
                }

                if (orderRecommended == "not assigned")
                {
                    logger.Error($"Unable to find recommended order for customer {customer.FirstName}" +
                        $" {customer.LastName}");
                }
                else
                {
                    Console.WriteLine($"Order recommended count: {mostFrequentOrder.Count()}");
                    Console.WriteLine($"Recommended Order for Customer {customer.FirstName}" +
                        $" {customer.LastName}: {orderRecommended}");
                }
            }
            else
            {
                logger.Error($"Unable to find recommended order for customer {customer.FirstName}" +
                        $" {customer.LastName}");
            }
        }

        public static string GetCustomerFirstName()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            Console.WriteLine("Please enter a first name:");
            var input = Console.ReadLine();
            if (!String.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                logger.Error("Empty input for first name is invalid.");
                return null;
            }
        }

        public static string GetCustomerLastName()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            Console.WriteLine("Please enter a last name:");
            var input = Console.ReadLine();
            if (!String.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                logger.Error("Empty input for last name is invalid.");
                return null;
            }
        }

        public static int GetLocation(IProject1Repo p0Repo, string prompt, int customerId)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            ConsoleDisplay.LocationList(p0Repo);
            Console.WriteLine();
            Console.WriteLine(prompt);
            var input = Console.ReadLine();

            if (input == "d" && customerId != -1)
            {
                return p0Repo.GetDefaultLocation(customerId);
            }
            else if (int.TryParse(input, out var locationId))
            {
                return locationId;
            }
            else
            {
                logger.Error($"Invalid input {input}");
                return -1;
            }
        }

        public static int GetCustomer(IProject1Repo p0Repo)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            ConsoleDisplay.CustomerList(p0Repo);
            Console.WriteLine();
            Console.WriteLine("Please enter a valid customer Id for the order:");
            var input = Console.ReadLine();

            if (int.TryParse(input, out var customerId))
            {
                return customerId;
            }
            else
            {
                logger.Error($"Invalid input {input}");
                return -1;
            }
        }

        public static Dictionary<int, int> GetCupcakes(IProject1Repo p0Repo)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            List<P1B.Cupcake> cupcakes = p0Repo.GetAllCupcakes().ToList();
            string input;
            Dictionary<int, int> cupcakeInputs = new Dictionary<int, int>();

            while (true)
            {
                ConsoleDisplay.CupcakeList(cupcakes);
                Console.WriteLine("Please enter the number of a cupcake that you would like to order\n" +
                "or 'C' to continue:");

                GetMenuInput(out input);

                if (input == "C")
                {
                    break;
                }
                else
                {
                    if (int.TryParse(input, out var cupcakeId))
                    {
                        if (!p0Repo.CheckCupcakeExists(cupcakeId))
                        {
                            logger.Error($"{cupcakeId} is not in the list of cupcakes.");
                            cupcakeInputs.Clear();
                            return cupcakeInputs;
                        }
                        if (cupcakeInputs.ContainsKey(cupcakeId))
                        {
                            logger.Error("You have already selected that cupcake in this order.");
                            Console.WriteLine();
                        }
                        else
                        {
                            cupcakeInputs[cupcakeId] = GetCupcakeQuantity();
                            if (cupcakeInputs[cupcakeId] == -1)
                            {
                                cupcakeInputs.Clear();
                                return cupcakeInputs;
                            }
                            // Remove cupcake from the temporary list of cupcakes
                            // to deter the user from re-entering it. If they re-enter it anyway
                            // they will get an error.
                            cupcakes.Remove(cupcakes.Single(c => c.Id == cupcakeId));
                        }
                    }
                    else
                    {
                        logger.Error($"Invalid input {input}");
                        cupcakeInputs.Clear();
                        return cupcakeInputs;
                    }
                }
            }
            return cupcakeInputs;
        }

        public static int GetCupcakeQuantity()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            Console.WriteLine("Please enter the quantity you would like to order:");
            var input = Console.ReadLine();

            if (int.TryParse(input, out var qnty))
            {
                if (!P1B.Order.CheckCupcakeQuantity(qnty))
                {
                    logger.Error("Order quantity for all cupcakes must be between 1 and 500.");
                    return -1;
                }
                return qnty;
            }
            else
            {
                logger.Error($"Invalid input {input}");
                return -1;
            }
        }

        public static void OrderDetails(IProject1Repo p0Repo)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            Console.WriteLine("Please enter an order Id:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var orderId))
            {
                if (!p0Repo.CheckOrderExists(orderId))
                {
                    logger.Error("That order is not in the system.");
                }
                else
                {
                    ConsoleDisplay.DisplayOrder(p0Repo.GetCupcakeOrder(orderId),
                        p0Repo.GetOrderItems(orderId).ToList(),
                        p0Repo.GetAllCupcakes().ToList());
                }
            }
            else
            {
                logger.Error($"Invalid input {input}");
            }
        }
    }
}
