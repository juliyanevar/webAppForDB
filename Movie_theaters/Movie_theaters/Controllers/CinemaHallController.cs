using Movie_theaters.Models;
using Movie_theaters.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Controllers
{
    public class CinemaHallController : Controller
    {
        private CinemaHallServices _cinemaHallServices;

        public ActionResult ListCinemaHall()
        {
            _cinemaHallServices = new CinemaHallServices();
            var model = _cinemaHallServices.GetCinemaHallList();
            return View(model);
        }


        public ActionResult AddCinemaHall()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCinemaHall(CinemaHallModel model)
        {
            _cinemaHallServices = new CinemaHallServices();
            _cinemaHallServices.InsertCinemaHall(model);

            return RedirectToAction("ListCinemaHall");
        }

        public ActionResult EditCinemaHall(int id)
        {
            _cinemaHallServices = new CinemaHallServices();
            var model = _cinemaHallServices.GetCinemaHallById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCinemaHall(CinemaHallModel model)
        {
            _cinemaHallServices = new CinemaHallServices();
            _cinemaHallServices.UpdateCinemaHall(model);
            return RedirectToAction("ListCinemaHall");
        }

        public ActionResult DeleteCinemaHall(int id)
        {
            _cinemaHallServices = new CinemaHallServices();
            _cinemaHallServices.DeleteCinemaHall(id);
            return RedirectToAction("ListCinemaHall");
        }
    }
}