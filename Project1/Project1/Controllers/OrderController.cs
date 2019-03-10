using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.BLL.IDataRepos;
using Project1.ViewModels;
using P1B = Project1.BLL;

namespace Project1.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ICustomerRepo customerRepo, ILocationRepo locationRepo,
            IOrderRepo orderRepo, ICupcakeRepo cupcakeRepo, IOrderItemRepo orderItemRepo,
            ILocationInventoryRepo locationInventoryRepo, IRecipeItemRepo recipeItemRepo,
            ILogger<OrderController> logger)
        {
            LocRepo = locationRepo;
            CustomerRepo = customerRepo;
            OrderRepo = orderRepo;
            CupcakeRepo = cupcakeRepo;
            OrderItemRepo = orderItemRepo;
            LocationInventoryRepo = locationInventoryRepo;
            RecipeItemRepo = recipeItemRepo;

            _logger = logger;
        }

        public ILocationRepo LocRepo { get; set; }
        public ICustomerRepo CustomerRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public ICupcakeRepo CupcakeRepo { get; set; }
        public IOrderItemRepo OrderItemRepo { get; set; }
        public ILocationInventoryRepo LocationInventoryRepo { get; set; }
        public IRecipeItemRepo RecipeItemRepo { get; set; }

        public ActionResult Index(string sortOrder)
        {
            ViewData["TimeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "time_desc" : "";
            ViewData["OrderTotalSortParm"] = sortOrder == "OrderTotal" ? "order_total_desc" : "OrderTotal";

            IEnumerable<P1B.Order> orders = OrderRepo.GetAllOrders();
            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            IEnumerable<P1B.Cupcake> cupcakes = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id);

            switch (sortOrder)
            {
                case "time_desc":
                    orders = orders.OrderByDescending(o => o.OrderTime);
                    break;
                case "OrderTotal":
                    orders = orders.OrderBy(o => o.GetTotalCost(OrderItemRepo.GetOrderItems(o.Id).ToList(),
                        cupcakes.ToList()));
                    break;
                case "order_total_desc":
                    orders = orders.OrderByDescending(o => o.GetTotalCost(OrderItemRepo.GetOrderItems(o.Id).ToList(),
                        cupcakes.ToList()));
                    break;
                default:
                    orders = orders.OrderBy(o => o.OrderTime);
                    break;
            }

            var viewModels = orders.Select(o => new OrderViewModel
            {
                OrderId = o.Id,
                LocationId = o.OrderLocation,
                LocationName = locations.Single(l => l.Id == o.OrderLocation).Name,
                CustomerId = o.OrderCustomer,
                CustomerName = customers.Single(c => c.Id == o.OrderCustomer).ReturnFullName(),
                OrderTime = o.OrderTime,
                Locations = locations.ToList(),
                Customers = customers.ToList(),
                Cupcakes = cupcakes.ToList(),
                OrderItems = OrderItemRepo.GetOrderItems(o.Id).ToList(),
                OrderTotal = OrderRepo.GetOrder(o.Id).GetTotalCost(OrderItemRepo.GetOrderItems(o.Id).ToList(),
                                        cupcakes.ToList())
            }).ToList();

            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            List<Project1.BLL.Cupcake> cupcakes = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id).ToList();
            List<Project1.BLL.OrderItem> orderItems = OrderItemRepo.GetOrderItems(id).ToList();
            Project1.BLL.Order order = OrderRepo.GetOrder(id);

            var viewModel = new OrderViewModel
            {
                OrderId = id,
                LocationId = order.OrderLocation,
                LocationName = locations.Single(l => l.Id == order.OrderLocation).Name,
                CustomerId = order.OrderCustomer,
                CustomerName = customers.Single(c => c.Id == order.OrderCustomer).ReturnFullName(),
                OrderTime = order.OrderTime,
                Locations = LocRepo.GetAllLocations().ToList(),
                Customers = CustomerRepo.GetAllCustomers().ToList(),
                Cupcakes = cupcakes,
                OrderItems = orderItems,
                OrderTotal = OrderRepo.GetOrder(order.Id).GetTotalCost(OrderItemRepo.GetOrderItems(order.Id).ToList(),
                                        cupcakes.ToList())
            };
            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            List<Project1.BLL.Cupcake> cupcakesTemp = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id).ToList();
            List<Project1.BLL.OrderItem> orderItemsTemp = new List<Project1.BLL.OrderItem>();
            foreach (var cupcake in cupcakesTemp)
            {
                orderItemsTemp.Add(new Project1.BLL.OrderItem
                {
                    Id = 0,
                    OrderId = 0,
                    CupcakeId = cupcake.Id,
                    Quantity = null
                });
            }

            var viewModel = new OrderViewModel
            {
                Locations = LocRepo.GetAllLocations().ToList(),
                Customers = CustomerRepo.GetAllCustomers().ToList(),
                Cupcakes = cupcakesTemp,
                OrderItems = orderItemsTemp
            };
            foreach (Project1.BLL.Customer customer in viewModel.Customers)
            {
                customer.FullName = customer.ReturnFullName();
            }
            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project1.ViewModels.OrderViewModel viewModel)
        {
            try
            {





                var newOrder = new P1B.Order
                {
                    OrderLocation = viewModel.LocationId,
                    OrderCustomer = viewModel.CustomerId,
                    OrderTime = DateTime.Now
                };
                OrderRepo.AddCupcakeOrder(newOrder);

                int newOrderId = OrderRepo.GetLastCupcakeOrderAdded();

                for (int i = 0; i < viewModel.OrderItems.Count; i++)
                {
                    viewModel.OrderItems[i].OrderId = newOrderId;
                    viewModel.OrderItems[i].CupcakeId = i + 1;
                }

                List<Project1.BLL.OrderItem> newOrderItems = viewModel.OrderItems
                                                    .Where(oi => oi.Quantity != null).ToList();
                OrderItemRepo.AddCupcakeOrderItems(newOrderItems);

                var recipes = RecipeItemRepo.GetRecipes(newOrderItems);
                LocationInventoryRepo.UpdateLocationInv(viewModel.LocationId, recipes, newOrderItems);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Order/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Order/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Order/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Order/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}