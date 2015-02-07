﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Web.Mvc;

using Web.Models.Home;

using BL.Services.Accounts;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel()
            {
                AccountsModel = GetAccountsPartialViewModel()
            };

            return View(model);
        }

        [HttpGet]
        [WebAuthorize(Role.Administrator)]
        public ActionResult Admin()
        {
            var model = new HomeAdminViewModel()
            {
                AccountsModel = GetAccountsPartialViewModel()
            };

            return View(model);
        }

        [HttpPost]
        [WebAuthorize(Role.Administrator)]
        [ValidateAntiForgeryToken]
        public ActionResult Admin(HomeAdminViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                var request = new CreateAccountRequest()
                {
                    Password = model.Password,
                    Role = model.UserRole,
                    Username = model.Username
                };

                var responce = SS.AccountService.Create(request);
                if (responce == true)
                {
                    return RedirectToAction("Admin");
                }

                ModelState.AddModelError("", responce.Message);
            }

            model.AccountsModel = GetAccountsPartialViewModel();
            return View(model);
        }

        [HttpGet]
        [WebAuthorize(Role.Manager)]
        public ActionResult Manager()
        {
            var model = new HomeManagerViewModel()
            {
                AccountsModel = GetAccountsPartialViewModel()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(string username, string password)
        {
            var account = SS.AccountService.ValidateLogin(username, password);
            if (account == null)
            {
                return RedirectToAction("Index");
            }

            SS.AuthenticationService.Authenticate(account.ID, false);

            if (account.Role == Role.Administrator)
            {
                return RedirectToAction("Admin");
            }

            return RedirectToAction("Manager");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            SS.AuthenticationService.DeAuthenticate();

            return RedirectToAction("Index");
        }

        private HomeAccountsPartialViewModel GetAccountsPartialViewModel()
        {
            var accounts = SS.AccountService.GetAllInContext();
            var model = new HomeAccountsPartialViewModel(accounts);

            return model;
        }
    }
}
