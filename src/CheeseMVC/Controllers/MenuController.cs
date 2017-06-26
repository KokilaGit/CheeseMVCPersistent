using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbcontext)
        {
            context = dbcontext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> cheeseMenus = context.Menus.ToList();
            return View(cheeseMenus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel menuVM = new AddMenuViewModel();
            return View(menuVM);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel menuVM)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = menuVM.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/"+newMenu.ID);
            }
            return View(menuVM);
        }

        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
        .CheeseMenus
        .Include(item => item.Cheese)
        .Where(cm => cm.MenuID == id)
        .ToList();
            Menu menu = context.Menus.Single(sm => sm.ID == id);

            ViewMenuViewModel newViewMenuVM = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };
            return View(newViewMenuVM);
        }
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> cheese = context.Cheeses.ToList();

            AddMenuItemViewModel addMenuItemVM = new AddMenuItemViewModel(menu, cheese);
            
            return View(addMenuItemVM);
        }
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemVM)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemVM.cheeseID;
                var menuID = addMenuItemVM.menuID;
               IList<CheeseMenu> existingItems = context.CheeseMenus
        .Where(cm => cm.CheeseID == cheeseID)
        .Where(cm => cm.MenuID == menuID).ToList();
                if (existingItems.Count == 0)
                {
                    CheeseMenu newCheese = new CheeseMenu
                    {
                        CheeseID = addMenuItemVM.cheeseID,
                        MenuID = addMenuItemVM.menuID,
                        Menu = context.Menus.Single(m => m.ID== addMenuItemVM.menuID),
                        Cheese = context.Cheeses.Single(c => c.ID== addMenuItemVM.cheeseID)
                    };
                    context.Add(newCheese);
                    context.SaveChanges();
                }
                else
                {
                    return View(addMenuItemVM);
                } 
            }
            return Redirect("/Menu/ViewMenu/" + addMenuItemVM.menuID);
        }
    }
}
