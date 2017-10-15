using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AnimalsShelter.Web.Models;
using AnimalsShelter.Data.Model;
using System;
using AnimalsShelter.Web.WebUtils.Contracts;
using Bytes2you.Validation;

namespace AnimalsShelter.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class AccountController : Controller
    {
        private readonly IVerificationProvider verificationProvider;

        public AccountController()
        {
        }

        public AccountController(IVerificationProvider verificationProvider )
        {
            Guard.WhenArgument(verificationProvider, nameof(verificationProvider)).IsNull().Throw();
            this.verificationProvider = verificationProvider;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (this.verificationProvider.IsAuthenticated)
            {
                return RedirectToAction("Index", "Shelter");
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewData["Title"] = "Log in";
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (this.verificationProvider.IsAuthenticated)
            {
                return RedirectToAction("Index", "Shelter");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = this.verificationProvider.GetUserByEmail(model.Email);
            if (user != null && user.DeletedOn != null)
            {
                ModelState.AddModelError("", "Your account is blocked!");
                return View(model);
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/Store/Index" : returnUrl;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = this.verificationProvider.SignInWithPassword(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.Redirect(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (this.verificationProvider.IsAuthenticated)
            {
                return RedirectToAction("Index", "Shelter");
            }

            ViewData["Title"] = "Register";
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (this.verificationProvider.IsAuthenticated)
            {
                return RedirectToAction("Index", "Shelter");
            }

            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                var result = this.verificationProvider.RegisterAndLoginUser(user, model.Password, isPersistent: false, rememberBrowser: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Shelter");
                }

                AddErrors(result);
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.verificationProvider.SignOut();
            return RedirectToAction("Index", "Shelter");
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
        #endregion
    }
}