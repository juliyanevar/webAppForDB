using Movie_theaters.Models;
using Movie_theaters.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Controllers
{
    public class SeanceController : Controller
    {
        private SeanceServices _seanceServices;
       
        public ActionResult GenerateSeances()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateSeances(SeanceModel model)
        {
            _seanceServices = new SeanceServices();
            _seanceServices.GenereateSeances(model.Date, model.Theater.Id);
            return RedirectToAction("GenerateSeances");
        }

        public ActionResult ListSeance()
        {
            _seanceServices = new SeanceServices();
            var model = _seanceServices.GetSeanceList();
            return View(model);
        }


        public ActionResult AddSeance()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSeance(SeanceModel model)
        {
            _seanceServices = new SeanceServices();
            _seanceServices.InsertSeance(model);

            return RedirectToAction("ListSeance");
        }

        public ActionResult EditSeance(int id)
        {
            _seanceServices = new SeanceServices();
            var model = _seanceServices.GetSeanceById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSeance(SeanceModel model)
        {
            _seanceServices = new SeanceServices();
            _seanceServices.UpdateSeance(model);
            return RedirectToAction("ListSeance");
        }

        public ActionResult DeleteSeance(int id)
        {
            _seanceServices = new SeanceServices();
            _seanceServices.DeleteSeance(id);
            return RedirectToAction("ListSeance");
        }

        public ActionResult SeanceForTheater(int id)
        {
            _seanceServices = new SeanceServices();
            var model = _seanceServices.GetSeanceForTheater(id);
            return View(model);
        }

        public ActionResult RedirectToTheaterList()
        {
            return RedirectToAction("ListTheaterForUser", "Theater");
        }

        public ActionResult RedirectToBooking(int id)
        {
            return RedirectToAction("TicketForSeance", "Ticket", new { id=id});
        }
    }
}