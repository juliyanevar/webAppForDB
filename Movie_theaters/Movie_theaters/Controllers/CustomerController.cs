using Movie_theaters.Controllers;
using Movie_theaters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie_theaters.Service
{
    public class CustomerController : Controller
    {
        private CustomerServices _customerServices;

        public ActionResult ListCustomer()
        {
            _customerServices = new CustomerServices();
            var model = _customerServices.GetCustomerList();
            return View(model);
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerModel model)
        {
            _customerServices = new CustomerServices();
            if (_customerServices.LoginExists(model.Login) != 0)
            {
                ModelState.AddModelError("Login", "This login already exists");
            }
            else if (_customerServices.EmailExists(model.Email) != 0)
            {
                ModelState.AddModelError("Email", "This email already in use");
            }
            else
            {
                _customerServices.InsertCustomer(model);
                return RedirectToAction("AddCustomer");
            }
            return View(model);
        }

        public ActionResult EditCustomer(int id)
        {
            _customerServices = new CustomerServices();
            var model = _customerServices.GetCustomerById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCustomer(CustomerModel model)
        {
            _customerServices = new CustomerServices();
            _customerServices.UpdateCustomer(model);
            return RedirectToAction("ListCustomer");
        }

        public ActionResult DeleteCustomer(int id)
        {
            _customerServices = new CustomerServices();
            _customerServices.DeleteCustomer(id);
            return RedirectToAction("ListCustomer");
        }

        public ActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(CustomerModel model)
        {
            _customerServices = new CustomerServices();
            if (_customerServices.Authorization(model) != 0)
            {
                if (_customerServices.GetRoleByLogin(model.Login) == "user")
                {
                    TempData["idUser"] =_customerServices.GetIdByLogin(model.Login);
                    return RedirectToAction("ListTheaterForUser", "Theater");
                }
                else
                {                 
                    return RedirectToAction("Index", "AdminPage");
                }
            }
            else
            {
                ModelState.AddModelError("Password", "Login or password entered incorrectly");
            }
            return View(model);
        }
    }
}