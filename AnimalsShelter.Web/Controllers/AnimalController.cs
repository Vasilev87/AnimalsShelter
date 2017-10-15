using AnimalsShelter.Data.Model;
using AnimalsShelter.Services.Contracts;
using AnimalsShelter.Web.ViewModels.Animals;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalsShelter.Web.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IAnimalsService animalsService;

        public AnimalController(IMapper mapper, IUsersService usersService, IAnimalsService animalsService)
        {
            this.mapper = mapper;
            this.usersService = usersService;
            this.animalsService = animalsService;
        }

        // Get Animals for Adoption
        [HttpGet]
        public ActionResult AnimalsForAdoption(int? page)
        {
            ViewData["Title"] = "Animals for Adoption";

            var animals = this.animalsService
                .GetAll()
                .Where(x => x.IsForAdoption == true)
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<AnimalsViewModel>()
                .ToList();

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(animals.ToPagedList(pageNumber, pageSize));
        }

        // Get Animals for Rehome
        [HttpGet]
        public ActionResult AnimalsForRehome(int? page)
        {
            ViewData["Title"] = "Animals for Adoption";

            var animals = this.animalsService
                .GetAll()
                .Where(x => x.IsForRehome == true)
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<AnimalsViewModel>()
                .ToList();

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(animals.ToPagedList(pageNumber, pageSize));
        }

        // Get Lost Animals
        [HttpGet]
        public ActionResult LostAnimals(int? page)
        {
            ViewData["Title"] = "Animals for Adoption";

            var animals = this.animalsService
                .GetAll()
                .Where(x => x.IsLost == true)
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<AnimalsViewModel>()
                .ToList();

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(animals.ToPagedList(pageNumber, pageSize));
        }

        // Get Found Animals
        [HttpGet]
        public ActionResult FoundAnimals(int? page)
        {
            ViewData["Title"] = "Animals for Adoption";

            var animals = this.animalsService
                .GetAll()
                .Where(x => x.IsFound == true)
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<AnimalsViewModel>()
                .ToList();

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(animals.ToPagedList(pageNumber, pageSize));
        }

        // Get All Animals
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
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService
                .GetAll()
                .SingleOrDefault(x => x.Id == userId);
            string path = GetFilePath(animal.Image);

            animal.ImagePath = path;
            animal.CreatedOn = DateTime.Now;
            animal.User = currentUser;
            this.animalsService.Add(animal);

            return RedirectToAction("Index", "Manage");
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

            return RedirectToAction("Index", "Manage");
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

            return RedirectToAction("Index", "Manage");
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

            return RedirectToAction("Index", "Manage");
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

            return RedirectToAction("Index", "Manage");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult DeleteAnimal(Guid id)
        {
            var animalToDelete = this.animalsService.GetAll()
                .Single(x => x.Id == id);

            this.animalsService.Delete(animalToDelete);

            return RedirectToAction("Index", "Manage");
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
                return imagePath;
            }
            else
            {
                return imagePath;
            }
        }
    }
}