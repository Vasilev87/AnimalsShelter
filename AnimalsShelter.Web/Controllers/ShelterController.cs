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

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        [ChildActionOnly]
        public ActionResult IndexCache()
        {
            return this.PartialView();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About US";

            return View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        [ChildActionOnly]
        public ActionResult AboutCache()
        {
            return this.PartialView();
        }

        public ActionResult Contact()
        {
            ViewData["Title"] = "Contact Us";

            return View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        [ChildActionOnly]
        public ActionResult ContactCache()
        {
            return this.PartialView();
        }
    }
}