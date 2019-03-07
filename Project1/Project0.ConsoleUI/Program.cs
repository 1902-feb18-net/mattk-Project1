using Microsoft.EntityFrameworkCore;
using NLog;
using Project0.DataAccess;
using Project1.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using P1B = Project1.BLL;

namespace Project0.ConsoleUI
{
    public class Program
    {
        /// <remarks>
        ///  The following command is the command in Package Manager Console to scaffold and re-scaffold the
        ///  database. I have had to do this 4-5 times already, so I have the command at the top of the
        ///  program so I can grab it.
        /// </remarks>
        // Scaffold-DbContext "<your-connection-string>" 
        //      Microsoft.EntityFrameworkCore.SqlServer
        //      -Project <name-of-data-project>

        /// <remarks>
        ///  The following line of code is commented out so that the SQL logging does not interfere with
        ///  using the console. Once the console UI is replaced with a website, I plan on turning 
        ///  the SQL logging to the console back on since there will no longer be a conflict.
        /// </remarks>
        /// <param name="args"></param>
        //public static readonly LoggerFactory AppLoggerFactory
        //    = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        static void Main(string[] args)
        {
            NLog.ILogger logger = LogManager.GetCurrentClassLogger();

            var optionsBuilder = new DbContextOptionsBuilder<Project0Context>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
            //optionsBuilder.UseLoggerFactory(AppLoggerFactory);
            var options = optionsBuilder.Options;

            using (var dbContext = new Project0Context(options))
            {
                IProject1Repo p0Repo = new Project0Repo(dbContext);

                Console.WriteLine("Welcome to Matt's Cupcakes Manager");
                Console.WriteLine();
                Console.WriteLine("Please select from the following options (not case-sensitive):");

                while (true)
                {
                    ConsoleDisplay.DisplayMenu();
                    ConsoleRead.GetMenuInput(out var input);
                    if (input == "L")
                    {
                        GetDataAndAddLocation(p0Repo);
                    }
                    else if (input == "C")
                    {
                        GetDataAndAddCustomer(p0Repo);
                    }
                    else if (input == "O")
                    {
                        GetDataAndAddOrder(p0Repo);
                    }
                    else if (input == "LL")
                    {
                        ConsoleDisplay.LocationList(p0Repo);
                    }
                    else if (input == "LO")
                    {
                        ConsoleRead.LocationOrders(p0Repo);
                    }
                    else if (input == "CL")
                    {
                        ConsoleDisplay.CustomerList(p0Repo);
                    }
                    else if (input == "CS")
                    {
                        ConsoleRead.CustomerSearch(p0Repo);
                    }
                    else if (input == "CO")
                    {
                        ConsoleRead.CustomerOrders(p0Repo);
                    }
                    else if (input == "OD")
                    {
                        ConsoleRead.OrderDetails(p0Repo);
                    }
                    else if (input == "OL")
                    {
                        ConsoleDisplay.OrderList(p0Repo, p0Repo.GetAllOrders().ToList(),
                            p0Repo.GetAllOrderItems().ToList(),
                            p0Repo.GetAllCupcakes().ToList(), p0Repo.GetAllLocations().ToList());
                    }
                    else if (input == "OR")
                    {
                        ConsoleRead.OrderRecommended(p0Repo);
                    }
                    else if (input == "Q")
                    {
                        break;
                    }
                }
            }
        }

        public static void GetDataAndAddLocation(IProject1Repo p0Repo)
        {
            // Add location requires no user input. It adds a store with an Id that is one more
            // than the last Id using the database.
            p0Repo.AddLocation();
            // Get the Id of the last store that was added, and report that to the user.
            int newLocationId = p0Repo.GetLastLocationAdded();
            p0Repo.FillLocationInventory(newLocationId);
            Console.WriteLine($"Location with Id of {newLocationId} successfully created!");
        }

        public static void GetDataAndAddCustomer(IProject1Repo p0Repo)
        {
            NLog.ILogger logger = LogManager.GetCurrentClassLogger();

            // Get all locations in order to validate if a customer can be added.
            // A location must exist in order for a customer to be added.
            var locations = p0Repo.GetAllLocations().ToList();
            if (locations.Count <= 0)
            {
                logger.Error("You must add at least one store location before you can add a customer.");
                return;
            }

            // Get a first name
            string fName = ConsoleRead.GetCustomerFirstName();
            if (fName is null) { return; }
            // Get a last name
            string lName = ConsoleRead.GetCustomerLastName();
            if (lName is null) { return; }
            Console.WriteLine();

            // Get a location for customer's default location
            int locationId = ConsoleRead.GetLocation(p0Repo,
                "Please enter a valid Id for default store location:", -1);
            if (locationId == -1) { return; }
            if (!p0Repo.CheckLocationExists(locationId))
            {
                logger.Error("The store location that you entered is not in the system.");
                return;
            }

            // Add the customer
            p0Repo.AddCustomer(fName, lName, locationId);
            // Get the Id of the customer that was just added and report that to the user
            int newCustomerId = p0Repo.GetLastCustomerAdded();
            Console.WriteLine($"Customer with Id of {newCustomerId} successfully created!");
        }

        public static void GetDataAndAddOrder(IProject1Repo p0Repo)
        {
            NLog.ILogger logger = LogManager.GetCurrentClassLogger();

            // Get all customers to validate that there is at least one customer.
            // There must be at least one customer to place an order.
            List<P1B.Customer> customers = p0Repo.GetAllCustomers().ToList();
            if (customers.Count == 0)
            {
                logger.Error("You have to add at least one customer before you can add an order.");
                return;
            }
            // Get a customer
            int customerId = ConsoleRead.GetCustomer(p0Repo);
            if (customerId == -1)
            {
                return;
            }
            if (!p0Repo.CheckCustomerExists(customerId))
            {
                logger.Error($"Customer {customerId} is not in the list of customers.");
                return;
            }
            // Get a location
            int locationId = ConsoleRead.GetLocation(p0Repo,
                "Please enter a valid store Id for the order or 'd' for customer default location:", customerId);
            if (locationId == -1)
            {
                return;
            }
            if (!p0Repo.CheckLocationExists(locationId))
            {
                logger.Error($"Location {locationId} is not in the list of stores.");
                return;
            }
            // The following checks to see if the customer can order from this store location
            // If the customer has ordered at this store within the past 2 hours, than they shouldn't be
            // able to order again.
            var orders = p0Repo.GetAllOrders().ToList();
            if (P1B.Customer.CheckCustomerCannotOrder(customerId, locationId, orders))
            {
                logger.Error("Customer can't place an order at this store because it hasn't been 2 hours \n" +
                    "since there last order yet.");
                return;
            }
            // Get some cupcakes from the user inputs, both type and quantity
            // These are placed in a dictionary.
            Dictionary<int, int> cupcakeInputs = ConsoleRead.GetCupcakes(p0Repo);
            if (cupcakeInputs.Count == 0)
            {
                return;
            }
            bool cupcakeFound = false;
            foreach (var item in cupcakeInputs)
            {
                if (item.Value > 0)
                {
                    cupcakeFound = true;
                    break;
                }
            }
            // Make sure that the user entered at least one cupcake, qnty pairing
            // No empty orders, no orders with zero quantities.
            if (!cupcakeFound)
            {
                logger.Error("You must enter at least one cupcake to place an order.");
                return;
            }
            // The following gets all orders and their associated order items in order
            // to validate that the store location's supply of the cupcakes in the customer's
            // requested order have not been exhausted.
            // Cupcake exhaustion happens when a store location has had more than 1000 cupcakes of one
            // type sold within 24 hours, at that point they cannot sell anymore.
            // This is arbitrary business logic that I added in order to satisfy the Project0
            // requirements.
            var orderItems = p0Repo.GetAllOrderItems().ToList();
            if (!P1B.Location.CheckCanOrderCupcake(locationId, cupcakeInputs, orders, orderItems))
            {
                logger.Error("This store has exhausted supply of that cupcake. Try back in 24 hours.");
                return;
            }
            // The following gets the recipes for the cupcakes in the customer's requested order
            // and checks to make sure that the store location's inventory can support the order.
            var recipes = p0Repo.GetRecipes(cupcakeInputs);
            var locationInv = p0Repo.GetLocationInv(locationId);
            if (!P1B.Location.CheckOrderFeasible(recipes, locationInv, cupcakeInputs))
            {
                logger.Error("This store does not have enough ingredients to place the requested order.");
                return;
            }

            // Add the cupcake order
            p0Repo.AddCupcakeOrder(locationId, customerId);
            // Get the new order Id that was just added in order to report it to the user.
            int newOrderId = p0Repo.GetLastCupcakeOrderAdded();
            // Add the order items to the cupcake order
            p0Repo.AddCupcakeOrderItems(newOrderId, cupcakeInputs);
            // Remove ingredient quantities from the store location's inventory
            p0Repo.UpdateLocationInv(locationId, recipes, cupcakeInputs);
            Console.WriteLine($"Order with Id of {newOrderId} successfully created!");
        }
    }
}

