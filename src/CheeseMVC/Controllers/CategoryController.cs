using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    public class CategoryController:Controller
    {
        private readonly CheeseDbContext context;
        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<CheeseCategory> cheeseCat =context.Categories.ToList();
            return View(cheeseCat);
        }

        public IActionResult Add()
        {
            AddCategoryViewModel newCategoryVm = new AddCategoryViewModel();
            return View(newCategoryVm);
        }
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel newCategoryVm)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory = new CheeseCategory
                {
                    Name = newCategoryVm.Name
                };
                context.Categories.Add(newCheeseCategory);
                context.SaveChanges();

                return Redirect("/Category");
            }

            return View(newCategoryVm);
        }
    }
}
