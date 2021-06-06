using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallAds.Database;
using SmallAds.Entities;
using SmallAds.Models.Home;
using System.Linq;

namespace SmallAds.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(HomeIndexVM model)
        {
            AdsDbContext context = new AdsDbContext();
            
            model.Ads = context.Ads.Include(u => u.Creator).Include(l => l.Likes).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {

            this.HttpContext.Session.Remove("LoggedUserId");
            this.HttpContext.Session.Remove("LoggedUserUsername");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(this.HttpContext.Session.GetString("LoggedUserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                    

            AdsDbContext context = new AdsDbContext();
            User loggedUser = context.Users.Where(u => u.Username == model.Username &&
                                                   u.Password == model.Password).FirstOrDefault();
            if(loggedUser == null)
            {
                ModelState.AddModelError("AutenticationFailed", "Wrong Username or Password");
                return View(model);
            }

            this.HttpContext.Session.SetString("LoggedUserId", loggedUser.Id.ToString());
            this.HttpContext.Session.SetString("LoggedUserUsername", loggedUser.Username);


            return RedirectToAction("Index", "Users");
        }
    }
}
