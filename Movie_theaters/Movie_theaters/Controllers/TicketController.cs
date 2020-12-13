using Movie_theaters.Models;
using Movie_theaters.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Controllers
{
    public class TicketController : Controller
    {
        private TicketServices _ticketServices;

        public ActionResult AddBooking(int id)
        {
            _ticketServices = new TicketServices();
            var model = _ticketServices.GetTicketById(id);
            _ticketServices.UpdateStatusTicket(model);
            int idUser = (int)TempData["idUser"];
            _ticketServices.AddBooking(idUser, model.Id);
            return RedirectToAction("TicketForSeance", new { id = model.Seance.Id});
        }


        public ActionResult TicketForSeance(int id)
        {
            _ticketServices = new TicketServices();
            var model = _ticketServices.GetTicketForSeance(id);
            return View(model);
        }
    }
}