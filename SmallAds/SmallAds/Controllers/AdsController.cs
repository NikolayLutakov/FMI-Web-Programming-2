using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallAds.Models.Ads;
using Microsoft.AspNetCore.Http;
using SmallAds.Database;
using SmallAds.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmallAds.Controllers
{
    public class AdsController : Controller
    {
        public IActionResult Index(IndexAdVM model)
        {
            AdsDbContext context = new AdsDbContext();
            model.Ads = context.Ads.Where(a => a.CreatorId == int.Parse(this.HttpContext.Session.GetString("LoggedUserId"))).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (this.HttpContext.Session.GetString("LoggedUserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //ViewBag.UserId = this.HttpContext.Session.GetString("LoggedUserId");

            CreateAdVM model = new CreateAdVM();
            model.CreatorId = int.Parse(this.HttpContext.Session.GetString("LoggedUserId"));

            return View(model);

        }

        [HttpPost]
        public IActionResult Create(CreateAdVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AdsDbContext context = new AdsDbContext();
            Ad ad = new Ad();

            ad.CreatorId = model.CreatorId;
            ad.Title = model.Title;
            ad.Text = model.Text;

            context.Add(ad);
            context.SaveChanges();

            return RedirectToAction("Index", "Ads");
        }

        public IActionResult Delete(int id)
        {
            AdsDbContext context = new AdsDbContext();

            List<Like> LikesToBeDeleted = context.Likes.Where(l => l.AdId == id).ToList();

            foreach (var item in LikesToBeDeleted)
            {
                context.Remove(item);
                context.SaveChanges();
            }
            

            Ad adToBeDeleted = context.Ads.Where(a => a.Id == id).FirstOrDefault();


            context.Remove(adToBeDeleted);
            context.SaveChanges();

            return RedirectToAction("Index", "Ads");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            AdsDbContext context = new AdsDbContext();
            Ad ad = context.Ads.Where(a => a.Id == id).FirstOrDefault();

            if (ad == null)
            {
                return RedirectToAction("Index", "Ads");
            }


            UpdateAdVM model = new UpdateAdVM();
            model.Id = ad.Id;
            model.CreatorId = ad.CreatorId;
            model.Title = ad.Title;
            model.Text = ad.Text;

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateAdVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AdsDbContext context = new AdsDbContext();

            Ad ad = new Ad();
            ad.Id = model.Id;
            ad.CreatorId = model.CreatorId;
            ad.Title = model.Title;
            ad.Text = model.Text;

            context.Ads.Update(ad);
            context.SaveChanges();


            return RedirectToAction("Index", "Ads");
        }

        public IActionResult Like(int adId, string callView)
        {
            int userId = int.Parse(this.HttpContext.Session.GetString("LoggedUserId"));

            AdsDbContext context = new AdsDbContext();

            Like item = new Like();
            item.AdId = adId;
            item.UserId = userId;

            context.Add(item);
            context.SaveChanges();

            if (callView == "usersIndex")
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("MyLikes", "Ads");
            }
        }

        public IActionResult Unlike(int adId, string callView)
        {
            int userId = int.Parse(this.HttpContext.Session.GetString("LoggedUserId"));
            AdsDbContext context = new AdsDbContext();

            Like itemToRemove = context.Likes.Where(a => a.UserId == userId && a.AdId == adId).FirstOrDefault();

            context.Remove(itemToRemove);
            context.SaveChanges();

            if (callView == "usersIndex")
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("MyLikes", "Ads");
            }
        }

        public IActionResult MyLikes()
        {
            AdsDbContext context = new AdsDbContext();

            int userId = int.Parse(this.HttpContext.Session.GetString("LoggedUserId"));

            MyLikesAdVM model = new MyLikesAdVM();

            model.User = context.Users.Include(l => l.Likes).ThenInclude(a => a.Ad).ThenInclude(u => u.Creator).Where(u => u.Id == userId).FirstOrDefault();

            return View(model);
        }
    }
}
