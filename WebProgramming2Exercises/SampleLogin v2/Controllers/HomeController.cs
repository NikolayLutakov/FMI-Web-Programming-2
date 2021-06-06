using System;
using Microsoft.AspNetCore.Mvc;
using SampleLogin.ViewModels.Home;

namespace SampleLogin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool isAuthenticated = false;
            if (model.Username == "nvalchanov" && model.Password == "nikolapass")
                isAuthenticated = true;
            else if (model.Username == "dblagoev" && model.Password == "dimitarpass")
                isAuthenticated = true;
            else if (model.Username == "psabev" && model.Password == "peterpass")
                isAuthenticated = true;

            if (!isAuthenticated)
            {
                ModelState.AddModelError("AuthenticationFailed", "Wrong username or password");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}