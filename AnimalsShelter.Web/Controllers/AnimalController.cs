using AnimalsShelter.Data.Model;
using AnimalsShelter.Services.Contracts;
using AnimalsShelter.Web.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
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

            var viewModel = new AnimalsViewModel()
            {
                Animals = animals
            };

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(viewModel.Animals.ToPagedList(pageNumber, pageSize));
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

            var viewModel = new AnimalsViewModel()
            {
                Animals = animals
            };

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(viewModel.Animals.ToPagedList(pageNumber, pageSize));
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

            var viewModel = new AnimalsViewModel()
            {
                Animals = animals
            };

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(viewModel.Animals.ToPagedList(pageNumber, pageSize));
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

            var viewModel = new AnimalsViewModel()
            {
                Animals = animals
            };

            var pageNumber = page ?? 1;
            var pageSize = 10;
            return View(viewModel.Animals.ToPagedList(pageNumber, pageSize));
        }

        // View Animal Details
        public ActionResult Computer(Guid Id)
        {
            ViewData["Title"] = "Animal Details";

            var animal = this.animalsService
                .GetAll()
                .ProjectTo<AnimalsViewModel>()
                .SingleOrDefault(x => x.Id == Id);

            return View(animal);
        }

        // Add Animal
        [HttpGet]
        [Authorize]
        public ActionResult AddAnimal()
        {
            ViewData["Title"] = "Add Animal";
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddComputer(Animal animal)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.usersService.GetAll()
                .SingleOrDefault(x => x.Id == userId);

            animal.CreatedOn = DateTime.Now;
            animal.User = currentUser;
            this.animalsService.Add(animal);

            return RedirectToAction("Index", "Manage");
        }

        // Update Animal Info
        [HttpGet]
        [Authorize]
        public ActionResult UpdateAnimal(Guid id)
        {
            ViewData["Title"] = "Update Computer Advertisement";

            var computer = this.animalsService
                .GetAll()
                .ProjectTo<AnimalsViewModel>()
                .Single(x => x.Id == id);

            return View(computer);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateComputer(Animal animal)
        {
            this.animalsService.Update(animal);

            return RedirectToAction("Index", "Manage");
        }
    }
}