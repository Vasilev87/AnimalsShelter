using AnimalsShelter.Web.ViewModels.Animals;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnimalsShelter.Services.Contracts;
using AutoMapper.QueryableExtensions;
using PagedList;
using AnimalsShelter.Data.Model;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System.IO;
using AnimalsShelter.Web.WebUtils.Contracts;
using Bytes2you.Validation;

namespace AnimalsShelter.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {

        private readonly IVerificationProvider verificationProvider;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IAnimalsService animalsService;

        public AdminPanelController(IVerificationProvider verificationProvider, IMapper mapper, IUsersService usersService, IAnimalsService animalsService)
        {
            Guard.WhenArgument(verificationProvider, nameof(verificationProvider)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            Guard.WhenArgument(animalsService, nameof(animalsService)).IsNull().Throw();

            this.verificationProvider = verificationProvider;
            this.mapper = mapper;
            this.usersService = usersService;
            this.animalsService = animalsService;
        }

        public AdminPanelController()
        {
        }

        // GET: Admin/AdminPanel
        public ActionResult Index()
        {
            ViewData["Title"] = "Admin Panel";

            return View();
        }

        // GET All Animals
        [HttpGet]
        public ActionResult Animals(int? page)
        {
            ViewData["Title"] = "Animals";

            var animals = this.animalsService
               .GetAll()
               .OrderByDescending(x => x.ModifiedOn)
               .ProjectTo<AnimalsViewModel>()
               .ToList();

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(animals.ToPagedList(pageNumber, pageSize));
        }

        // View Animal Details
        [HttpGet]
        public ActionResult Animal(Guid Id)
        {
            ViewData["Title"] = "Animal Details";

            var animal = this.animalsService
                .GetAll()
                .ProjectTo<AnimalsViewModel>()
                .Single(x => x.Id == Id);

            return View(animal);
        }

        // Add Animal for Adoption
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAnimalForAdoption()
        {
            ViewData["Title"] = "Add Animal for Adoption";
            return View(new AnimalsViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnimalForAdoption(Animal animal)
        {
            var userId = this.verificationProvider.CurrentUserId;
            var currentUser = this.usersService
                .GetAll()
                .SingleOrDefault(x => x.Id == userId);
            string path = GetFilePath(animal.Image);

            animal.ImagePath = path;
            animal.CreatedOn = DateTime.Now;
            animal.User = currentUser;
            this.animalsService.Add(animal);

            return RedirectToAction("Index", "AdminPanel");
        }

        // Add Animal for Rehome
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult AddAnimalForRehome()
        {
            ViewData["Title"] = "Add Animal for Adoption";
            return View(new AnimalsViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnimalForRehome(Animal animal)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService
                .GetAll()
                .SingleOrDefault(x => x.Id == userId);
            string path = GetFilePath(animal.Image);

            animal.ImagePath = path;
            animal.CreatedOn = DateTime.Now;
            animal.User = currentUser;
            this.animalsService.Add(animal);

            return RedirectToAction("Index", "AdminPanel");
        }

        // Add Lost Animal
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult AddLostAnimal()
        {
            ViewData["Title"] = "Add Animal for Adoption";
            return View(new AnimalsViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public ActionResult AddLostAnimal(Animal animal)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService
                .GetAll()
                .SingleOrDefault(x => x.Id == userId);
            string path = GetFilePath(animal.Image);

            animal.ImagePath = path;
            animal.CreatedOn = DateTime.Now;
            animal.User = currentUser;
            this.animalsService.Add(animal);

            return RedirectToAction("Index", "AdminPanel");
        }

        // Add Found Animal
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult AddFoundAnimal()
        {
            ViewData["Title"] = "Add Animal for Adoption";
            return View(new AnimalsViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public ActionResult AddFoundAnimal(Animal animal)
        {

            string path = GetFilePath(animal.Image);
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService
                .GetAll()
                .SingleOrDefault(x => x.Id == userId);

            animal.ImagePath = path;
            animal.CreatedOn = DateTime.Now;
            animal.User = currentUser;
            this.animalsService.Add(animal);

            return RedirectToAction("Index", "AdminPanel");
        }

        // Update Animal Info
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult UpdateAnimal(Guid id)
        {
            ViewData["Title"] = "Update Animal Info";

            var animal = this.animalsService
                .GetAll()
                .ProjectTo<AnimalsViewModel>()
                .Single(x => x.Id == id);

            return View(animal);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAnimal(Animal animal)
        {
            string path = GetFilePath(animal.Image);
            animal.ImagePath = path;
            this.animalsService.Update(animal);

            return RedirectToAction("Animals", "AdminPanel");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult DeleteAnimal(Guid id)
        {
            var animalToDelete = this.animalsService.GetAll()
                .Single(x => x.Id == id);

            this.animalsService.Delete(animalToDelete);

            return RedirectToAction("Animals", "AdminPanel");
        }

        private string GetFilePath(HttpPostedFileBase image)
        {
            string imagePath = string.Empty;

            if (image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(image.FileName);
                string fullPath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                image.SaveAs(fullPath);

                imagePath = "~/UploadedFiles/" + fileName;
            }
            return imagePath;
        }
    }
}