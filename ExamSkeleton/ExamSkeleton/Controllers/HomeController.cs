using ExamSkeleton.Data;
using ExamSkeleton.Data.Entities;
using ExamSkeleton.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSkeleton.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(IndexVM model)
        {
            SkeletonDbContext context = new SkeletonDbContext();


            model.SomeEntities = context.SomeEntities.ToList();

            return View(model);
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

            SkeletonDbContext context = new SkeletonDbContext();

            SomeEntity entityToAdd = new SomeEntity();

            entityToAdd.ColumnOne = model.ColumnOne;
            entityToAdd.ColumnTwo = model.ColumnTwo;
            entityToAdd.ColumnThree = model.ColumnThree;
            entityToAdd.ColumnFour = model.ColumnFour;

            context.Add(entityToAdd);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            SkeletonDbContext context = new SkeletonDbContext();

            var item = context.SomeEntities.Find(id);

            if (item == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new UpdateVM();

            model.Id = item.Id;
            model.ColumnOne = item.ColumnOne;
            model.ColumnTwo = item.ColumnTwo;
            model.ColumnThree = item.ColumnThree;
            model.ColumnFour = item.ColumnFour;

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(UpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SkeletonDbContext context = new SkeletonDbContext();

            var itemToUpdate = new SomeEntity();

            itemToUpdate.Id = model.Id;
            itemToUpdate.ColumnOne = model.ColumnOne;
            itemToUpdate.ColumnTwo = model.ColumnTwo;
            itemToUpdate.ColumnThree = model.ColumnThree;
            itemToUpdate.ColumnFour = model.ColumnFour;

            context.Update(itemToUpdate);
            context.SaveChanges();


            return RedirectToAction("Index", "Home");
        }


        public IActionResult Delete(int id)
        {
            SkeletonDbContext context = new SkeletonDbContext();

            var itemToDelete = context.SomeEntities.Find(id);

            context.Remove(itemToDelete);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
