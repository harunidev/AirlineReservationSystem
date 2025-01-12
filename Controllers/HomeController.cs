using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFinal.Data;

namespace ProjectFinal.Controllers
{
    public class HomeController : Controller
    {
        private ProjectFinalContext db = new ProjectFinalContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Flight_List()
        {
            var flights = db.Flights.ToList();
            return View("Flight_List", flights);
        }



        public ActionResult Flight_Info(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var flight = db.Flights.FirstOrDefault(f => f.ID == id);

            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        [HttpPost]

        public ActionResult ReserveFlight(int id)
        {
            // Fetch the flight by ID
            var flight = db.Flights.FirstOrDefault(f => f.ID == id);

            if (flight == null)
            {
                TempData["Message"] = "Flight not found.";
                return RedirectToAction("Flight_Info", new { id });
            }

            if (!flight.F_Available)
            {
                TempData["Message"] = "Flight is already reserved.";
                return RedirectToAction("Flight_Info", new { id });
            }

            // Reserve the flight
            flight.F_Available = false;
            db.SaveChanges();

            TempData["Message"] = "Flight reserved successfully!";
            return RedirectToAction("Flight_Info", new { id });
        }
        public ActionResult Flight_List_Data(string firm, string flightClass, int? people)
        {
            var flights = db.Flights.AsQueryable();

            // Filter by Plane Model
            if (!string.IsNullOrEmpty(firm))
            {
                flights = flights.Where(f => f.Plane_model == firm);
            }

            // Filter by Class (Business/Economy)
            if (!string.IsNullOrEmpty(flightClass))
            {
                try
                {
                    var classType = (ProjectFinal.Models.Type)Enum.Parse(typeof(ProjectFinal.Models.Type), flightClass, true);
                    flights = flights.Where(f => f.F_Class == classType);
                }
                catch
                {
                    // Return an empty list on invalid flightClass
                    return PartialView("_FlightList", new List<ProjectFinal.Models.Flights>());
                }
            }

            // Filter by number of people
            if (people.HasValue)
            {
                flights = flights.Where(f => f.peoples >= people.Value);
            }

            // Convert to list and return partial view
            return PartialView("_FlightListPartial", flights.ToList());
        }
    }
}
