using Movie_theaters.Models;
using Movie_theaters.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Controllers
{
    public class GenreController : Controller
    {
        private GenreServices _genreServices;

        public ActionResult ListGenre()
        {
            _genreServices = new GenreServices();
            var model = _genreServices.GetGenreList();
            return View(model);
        }


        public ActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGenre(GenreModel model)
        {
            _genreServices = new GenreServices();
            _genreServices.InsertGenre(model);

            return RedirectToAction("ListGenre");
        }

        public ActionResult EditGenre(int id)
        {
            _genreServices = new GenreServices();
            var model = _genreServices.GetGenreById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditGenre(GenreModel model)
        {
            _genreServices = new GenreServices();
            _genreServices.UpdateGenre(model);
            return RedirectToAction("ListGenre");
        }

        public ActionResult DeleteGenre(int id)
        {
            _genreServices = new GenreServices();
            _genreServices.DeleteGenre(id);
            return RedirectToAction("ListGenre");
        }
    }
}