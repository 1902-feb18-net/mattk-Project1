using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.BLL.IDataRepos;
using Project1.ViewModels;
using P1B = Project1.BLL;

namespace Project1.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(ICustomerRepo customerRepo, ILocationRepo locationRepo,
            IOrderRepo orderRepo, ICupcakeRepo cupcakeRepo)
        {
            LocRepo = locationRepo;
            CustomerRepo = customerRepo;
            OrderRepo = orderRepo;
            CupcakeRepo = cupcakeRepo;
        }

        public ILocationRepo LocRepo { get; set; }
        public ICustomerRepo CustomerRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public ICupcakeRepo CupcakeRepo { get; set; }


        // GET: Order
        public ActionResult Index()
        {
            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            IEnumerable<P1B.Order> orders = OrderRepo.GetAllOrders();

            var viewModels = orders.Select(o => new OrderViewModel
            {
                OrderId = o.Id,
                LocationId = o.OrderLocation,
                LocationName = locations.Single(l => l.Id == o.OrderLocation).Name,
                CustomerId = o.OrderCustomer,
                CustomerName = customers.Single(c => c.Id == o.OrderCustomer).ReturnFullName(),
                OrderTime = o.OrderTime
            }).ToList();

            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                // TODO: Add insert logic here
                var newOrder = new P1B.Order
                {
                    OrderLocation = viewModel.LocationId,
                    OrderCustomer = viewModel.CustomerId,
                    OrderTime = DateTime.Now
                };

                // TODO: Add insert logic here
                //OrderRepo.AddCupcakeOrder(newOrder);

                List<Project1.BLL.OrderItem> newOrderItems = viewModel.OrderItems.ToList();
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