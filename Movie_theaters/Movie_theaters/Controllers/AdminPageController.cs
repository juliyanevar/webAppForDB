using System;
using Movie_theaters.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Controllers
{
    public class AdminPageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RedirectToTheaters()
        {
            return RedirectToAction("ListTheater", "Theater");
        }

        public ActionResult RedirectToMovies()
        {
            return RedirectToAction("ListMovie", "Movie");
        }

        public ActionResult RedirectToGenres()
        {
            return RedirectToAction("ListGenre", "Genre");
        }

        public ActionResult RedirectToCinemaHalls()
        {
            return RedirectToAction("ListCinemaHall", "CinemaHall");
        }

        public ActionResult RedirectToSeances()
        {
            return RedirectToAction("ListSeance", "Seance");
        }
    }
}