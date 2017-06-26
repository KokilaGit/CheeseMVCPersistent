using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        [Required]
        [Display(Name ="Select Cheese ")]
        public int cheeseID { get; set; }

        public int menuID { get; set; }

        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel() { }
        public AddMenuItemViewModel(Menu menu,IEnumerable<Cheese> cheese)
        {
            menuID = menu.ID;
            Menu = menu;
            Cheeses = new List<SelectListItem>();
            foreach(Cheese  c in cheese)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                });

            }
            
        }
    }
}
