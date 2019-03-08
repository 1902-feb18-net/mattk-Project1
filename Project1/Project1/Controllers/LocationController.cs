using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.BLL;
using Project1.DataAccess;
using P1B = Project1.BLL;

namespace Project1.Controllers
{
    public class LocationController : Controller
    {

        public LocationController(ILocationRepo locationRepo)
        {
            LocRepo = locationRepo;
        }

        public ILocationRepo LocRepo { get; set; }

        // GET: Location
        public ActionResult Index()
        {
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            return View(locations);
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            P1B.Location movie = LocRepo.GetLocationById(id);
            return View(movie);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Location/Edit/5
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