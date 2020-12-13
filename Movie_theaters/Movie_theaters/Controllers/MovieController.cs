using Movie_theaters.Models;
using Movie_theaters.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Controllers
{
    public class MovieController : Controller
    {
        private MovieServices _movieServices;

        public ActionResult ListMovie()
        {
            _movieServices = new MovieServices();
            var model = _movieServices.GetMovieList();
            return View(model);
        }


        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(MovieModel model)
        {
            _movieServices = new MovieServices();
            _movieServices.InsertMovie(model);

            return RedirectToAction("ListMovie");
        }

        public ActionResult EditMovie(int id)
        {
            _movieServices = new MovieServices();
            var model = _movieServices.GetMovieById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditMovie(MovieModel model)
        {
            _movieServices = new MovieServices();
            _movieServices.UpdateMovie(model);
            return RedirectToAction("ListMovie");
        }

        public ActionResult DeleteMovie(int id)
        {
            _movieServices = new MovieServices();
            _movieServices.DeleteMovie(id);
            return RedirectToAction("ListMovie");
        }

        public ActionResult AddGenre(int id)
        {
            _movieServices = new MovieServices();
            var model = _movieServices.GetMovieById(id);
            var genreModel = new GenreModel();
            RedirectToAction("AddGenre", "Movie", new { movieModel = model, genreModel = genreModel });
            return View(genreModel);
        }

        [HttpPost]
        public ActionResult AddGenre(MovieModel movieModel, GenreModel genreModel)
        {
            _movieServices = new MovieServices();
            _movieServices.AddGenreForMovie(movieModel.Id, genreModel.Name);

            return RedirectToAction("ListMovie");
        }
    }
}