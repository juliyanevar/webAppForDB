using Movie_theaters.Models;
using Movie_theaters.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Controllers
{
    public class TheaterController : Controller
    {
        private TheaterServices _theaterServices;

        public ActionResult ListTheater()
        {
            _theaterServices = new TheaterServices();
            var model = _theaterServices.GetTheaterList();
            return View(model);
        }

        public ActionResult ListTheaterForUser()
        {
            _theaterServices = new TheaterServices();
            var model = _theaterServices.GetTheaterList();
            return View(model);
        }


        public ActionResult AddTheater()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTheater(TheaterModel model)
        {
            _theaterServices = new TheaterServices();
            _theaterServices.InsertTheater(model);

            return RedirectToAction("ListTheater");
        }

        public ActionResult EditTheater(int id)
        {
            _theaterServices = new TheaterServices();
            var model = _theaterServices.GetTheaterById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditTheater(TheaterModel model)
        {
            _theaterServices = new TheaterServices();
            _theaterServices.UpdateTheater(model);
            return RedirectToAction("ListTheater");
        }

        public ActionResult DeleteTheater(int id)
        {
            _theaterServices = new TheaterServices();
            _theaterServices.DeleteTheater(id);
            return RedirectToAction("ListTheater");
        }

        public ActionResult RedirectToSeances(int id)
        {
            return RedirectToAction("SeanceForTheater", "Seance", new { id=id});
        }
    }
}