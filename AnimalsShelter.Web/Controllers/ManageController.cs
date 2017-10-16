using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AnimalsShelter.Web.Models;
using AutoMapper;
using AnimalsShelter.Services.Contracts;
using AutoMapper.QueryableExtensions;
using PagedList;
using AnimalsShelter.Web.ViewModels.Animals;
using AnimalsShelter.Web.WebUtils.Contracts;
using Bytes2you.Validation;

namespace AnimalsShelter.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ManageController : Controller
    {
        private readonly IVerificationProvider verificationProvider;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IAnimalsService animalsService;

        public ManageController()
        {
        }

        public ManageController(IVerificationProvider verificationProvider, IMapper mapper, IUsersService usersService, IAnimalsService animalsService)
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

        //
        // GET: /Manage/Index
        [HttpGet]
        public ActionResult Index(ManageMessageId? message, int? page)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            ViewData["Title"] = "Manage";

            var userId = this.verificationProvider.CurrentUserId;

            var animals = this.animalsService
                .GetAll()
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<AnimalsViewModel>()
                .ToList();

            var pageNumber = page ?? 1;
            var pageSize = 10;

            return View(animals.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            ViewData["Title"] = "Change Password";
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = this.verificationProvider.ChangePassword(this.verificationProvider.CurrentUserId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = this.verificationProvider.GetUserById(this.verificationProvider.CurrentUserId);
                if (user != null)
                {
                    this.verificationProvider.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}