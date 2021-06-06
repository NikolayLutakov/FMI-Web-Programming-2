using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallAds.Database;
using SmallAds.Entities;
using SmallAds.Models.Home;
using SmallAds.Models.Users;

namespace SmallAds.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index(IndexVM model)
        {
            if (this.HttpContext.Session.GetString("LoggedUserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            AdsDbContext context = new AdsDbContext();

            model.User = context.Users.Where(u => u.Id == int.Parse(this.HttpContext.Session.GetString("LoggedUserId"))).FirstOrDefault();
            
            model.Ads = context.Ads.Include(u => u.Creator).Include(l => l.Likes).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            AdsDbContext context = new AdsDbContext();

            
            User item = context.Users.Include(x => x.Address).ThenInclude(x => x.Town).Where(u => u.Id == id).FirstOrDefault();

            if(item == null)
            {
                return RedirectToAction("Index", "Users");
            }

            UpdateVM model = new UpdateVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.Email = item.Email;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;
            model.Phone = item.Phone;
            model.Address = item.Address.AddressText;
            model.Town = item.Address.Town.Name;

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AdsDbContext context = new AdsDbContext();

            List<string> emails = context.Users.Where(u => u.Username != model.Username).Select(u => u.Email).ToList();

            if (emails.Contains(model.Email))
            {
                ModelState.AddModelError("EmailTaken", "This email is already taken!");
                return View(model);
            }

            Town town = new Town();
            string townFirstLetter = model.Town.Substring(0, 1).ToUpper();
            string name = String.Concat(townFirstLetter, model.Town.Substring(1, model.Town.Length - 1));
            town.Name = name;

            int insertedTtownId;
            List<string> towns = context.Towns.Select(t => t.Name.ToLower()).ToList();
            if (towns.Contains(town.Name.ToLower()))
            {
                insertedTtownId = context.Towns.Where(c => c.Name == town.Name).Select(t => t.Id).FirstOrDefault();
            }
            else
            {
                context.Add(town);
                context.SaveChanges();

                insertedTtownId = town.Id;
            }


            int addressId = context.Users.Include(x => x.Address).Where(u => u.Id == model.Id).Select(x => x.AddressId).FirstOrDefault();
            //string addressId = context.Users.Join(context.Addresesses, u => u.AddressId,
            //                                    a => a.Id,
            //                                    (u, a) => new { a.Id }).Where(u => u.Id == model.Id).ToString();

            Addresess address = new Addresess();
            address.Id = addressId;
            address.TownId = insertedTtownId;
            address.AddressText = model.Address;

            context.Update(address);
            context.SaveChanges();

            int updatedAddress = addressId;


            User item = new User();
            item.Id = model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.Email = model.Email;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Phone = model.Phone;
            item.AddressId = updatedAddress;

            

            context.Users.Update(item);
            context.SaveChanges();
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            AdsDbContext context = new AdsDbContext();

            List<string> users = context.Users.Select(u => u.Username).ToList();
            List<string> emails = context.Users.Select(u => u.Email).ToList();

            if (users.Contains(model.Username))
            {
                ModelState.AddModelError("UsernameTaken", "This username is already taken!");
                return View(model);
            }

            if (emails.Contains(model.Email))
            {
                ModelState.AddModelError("EmailTaken", "This email is already taken!");
                return View(model);
            }

            

            Town town = new Town();
            string townFirstLetter = model.Town.Substring(0, 1).ToUpper();
            string name = String.Concat(townFirstLetter, model.Town.Substring(1, model.Town.Length - 1));
            town.Name = name;

            int insertedTtownId;
            List<string> towns = context.Towns.Select(t => t.Name.ToLower()).ToList();
            if (towns.Contains(town.Name.ToLower()))
            {
                insertedTtownId = context.Towns.Where(c => c.Name == town.Name).Select(t => t.Id).FirstOrDefault();
            }
            else
            {
                context.Add(town);
                context.SaveChanges();

                insertedTtownId = town.Id;
            }

            Addresess address = new Addresess();
            address.TownId = insertedTtownId;
            address.AddressText = model.Address;

            context.Add(address);
            context.SaveChanges();

            int insertedAddressId = address.Id;

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.Email = model.Email;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Phone = model.Phone;
            item.AddressId = insertedAddressId;


            context.Add(item);
            context.SaveChanges();

            //LoginVM newUser = new LoginVM();
            //newUser.Username = item.Username;
            //newUser.Password = item.Password;
            //var result = new HomeController();
            
            //result.HttpContext.Session.SetString("LoggedUserId", item.Id.ToString());
            //return result.Login(newUser);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            AdsDbContext context = new AdsDbContext();

            List<Like> UserLikesToBeDeleted = context.Likes.Where(l => l.UserId == id).ToList();

            foreach (var item in UserLikesToBeDeleted)
            {
                context.Remove(item);
                context.SaveChanges();
            }

            List<Ad> adsToBeDeleted = context.Ads.Where(a => a.CreatorId == id).ToList();

            foreach (var item in adsToBeDeleted)
            {
                List<Like> adLikesToBeDeleted = context.Likes.Where(x => x.AdId == item.Id).ToList();
                
                foreach (var like in adLikesToBeDeleted)
                {
                    context.Remove(like);
                    context.SaveChanges();
                }

                context.Remove(item);
                context.SaveChanges();
            }

            User user = context.Users.Where(u => u.Id == id).FirstOrDefault();
            Addresess address = context.Addresesses.Where(a => a.Id == user.AddressId).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            context.Remove(address);
            context.Remove(user);

            context.SaveChanges();

            return RedirectToAction("Logout", "Home");
        }
    }
}
