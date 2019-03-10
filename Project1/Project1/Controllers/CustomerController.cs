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
    public class CustomerController : Controller
    {
        public CustomerController(ICustomerRepo customerRepo, ILocationRepo locationRepo)
        {
            LocRepo = locationRepo;
            CustomerRepo = customerRepo;
        }

        public ILocationRepo LocRepo { get; set; }
        public ICustomerRepo CustomerRepo { get; set; }

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();

            var viewModels = customers.Select(c => new CustomerViewModel
            {
                CustomerId = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DefaultLocation = c.DefaultLocation,
                DefaultLocationName = locations.Single(l => l.Id == c.DefaultLocation).Name
            }).ToList();

            return View(viewModels);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var viewModel = new CustomerViewModel
            {
                Locations = LocRepo.GetAllLocations().ToList()
            };
            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(P1B.Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                var newCustomer = new P1B.Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DefaultLocation = customer.DefaultLocation ?? null
                };

                // TODO: Add insert logic here
                CustomerRepo.AddCustomer(newCustomer);
                return RedirectToAction(nameof(Index));
                // TODO: Add insert logic here
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}