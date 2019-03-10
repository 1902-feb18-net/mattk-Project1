using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.BLL.IDataRepos;
using P1B = Project1.BLL;
using Project1.ViewModels;
using System.Text.RegularExpressions;

namespace Project1.Controllers
{
    public class LocationController : Controller
    {
        public LocationController(ICustomerRepo customerRepo, ILocationRepo locationRepo,
            IOrderRepo orderRepo, ICupcakeRepo cupcakeRepo, IOrderItemRepo orderItemRepo,
            ILocationInventoryRepo locationInventoryRepo, IRecipeItemRepo recipeItemRepo,
            IIngredientRepo ingRepo)
        {
            LocRepo = locationRepo;
            CustomerRepo = customerRepo;
            OrderRepo = orderRepo;
            CupcakeRepo = cupcakeRepo;
            OrderItemRepo = orderItemRepo;
            LocationInventoryRepo = locationInventoryRepo;
            RecipeItemRepo = recipeItemRepo;
            IngRepo = ingRepo;
        }

        public ILocationRepo LocRepo { get; set; }
        public ICustomerRepo CustomerRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public ICupcakeRepo CupcakeRepo { get; set; }
        public IOrderItemRepo OrderItemRepo { get; set; }
        public ILocationInventoryRepo LocationInventoryRepo { get; set; }
        public IRecipeItemRepo RecipeItemRepo { get; set; }
        public IIngredientRepo IngRepo { get; set; }

        // GET: Location
        public ActionResult Index()
        {
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            return View(locations);
        }

        // GET: Location/Details/5
        [Route("[controller]/Inventory/{id?}")]
        public ActionResult Inventory(int id)
        {
            P1B.Location currentLocation = LocRepo.GetLocationById(id);
            ViewData["currentLocation"] = currentLocation.Name;

            IEnumerable<P1B.LocationInventory> locInvs = LocationInventoryRepo.GetLocationInventoryByLocationId(id)
                .OrderBy(li => li.IngredientId);
            List<P1B.Ingredient> ings = IngRepo.GetIngredients().ToList();

            var viewModels = locInvs.Select(li => new LocationInventoryViewModel
            {
                LocationId = id,
                LocationName = currentLocation.Name,
                IngredientId = li.IngredientId,
                // https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
                IngredientType = Regex.Replace(ings.Single(i => i.Id == li.IngredientId).Type, "([a-z])([A-Z])", "$1 $2"),
                IngredientUnits = ings.Single(i => i.Id == li.IngredientId).Units,
                IngredientAmount = li.Amount
            }).ToList();

            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Orders(int id)
        {
            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            List<Project1.BLL.Cupcake> cupcakes = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id).ToList();
            List<Project1.BLL.Order> orders = LocRepo.GetLocationOrderHistory(id).ToList();
            List<OrderViewModel> viewModels = new List<OrderViewModel>();

            ViewData["LocationName"] = locations.Single(l => l.Id == id).Name;

            foreach (var order in orders)
            {
                viewModels.Add(new OrderViewModel
                {
                    OrderId = order.Id,
                    LocationId = order.OrderLocation,
                    LocationName = locations.Single(l => l.Id == order.OrderLocation).Name,
                    CustomerId = order.OrderCustomer,
                    CustomerName = customers.Single(c => c.Id == order.OrderCustomer).ReturnFullName(),
                    OrderTime = order.OrderTime,
                    Locations = locations.ToList(),
                    Customers = customers.ToList(),
                    Cupcakes = cupcakes,
                    OrderItems = OrderItemRepo.GetOrderItems(order.Id).ToList()
            });
            }

            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Order(int id)
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
                OrderItems = orderItems
            };
            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(P1B.Location location)
        {
            try
            {
                // TODO: Add insert logic here
                var newLocation = new P1B.Location
                {
                    Name = location.Name
                };

                // TODO: Add insert logic here
                LocRepo.AddLocation(location);
                int newLocationId = LocRepo.GetLastLocationAdded();
                LocationInventoryRepo.FillLocationInventory(newLocationId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Location/Edit/5
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

        //// GET: Location/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Location/Delete/5
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