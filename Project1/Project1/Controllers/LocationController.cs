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

        public LocationController(ILocationRepo locationRepo, ILocationInventoryRepo locInvRepo,
            IIngredientRepo ingRepo)
        {
            LocRepo = locationRepo;
            LocInvRepo = locInvRepo;
            IngRepo = ingRepo;
        }

        public ILocationRepo LocRepo { get; set; }
        public ILocationInventoryRepo LocInvRepo { get; set; }
        public IIngredientRepo IngRepo { get; set; }

        // GET: Location
        public ActionResult Index()
        {
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            return View(locations);
        }

        // GET: Location/Details/5
        public ActionResult Inventory(int id)
        {
            P1B.Location currentLocation = LocRepo.GetLocationById(id);
            ViewData["currentLocation"] = currentLocation.Name;

            IEnumerable<P1B.LocationInventory> locInvs = LocInvRepo.GetLocationInventoryByLocationId(id)
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
                LocInvRepo.FillLocationInventory(newLocationId);
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