using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalsShelter.Web.Controllers
{
    public class ShelterController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Animals Shelter";

            return View();
        }

        public ActionResult IndexCache()
        {
            return this.PartialView();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About US";

            return View();
        }

        public ActionResult AboutCache()
        {
            return this.PartialView();
        }

        public ActionResult Contact()
        {
            ViewData["Title"] = "Contact Us";

            return View();
        }

        public ActionResult ContactCache()
        {
            return this.PartialView();
        }
    }
}