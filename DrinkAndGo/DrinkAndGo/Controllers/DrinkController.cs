using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkAndGo.Data.interfaces;
using DrinkAndGo.Data.Models;
using DrinkAndGo.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrinkAndGo.Controllers
{
    public class DrinkController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DrinkController(IDrinkRepository drinkRepository, ICategoryRepository categoryRepository)
        {
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
        }

        #region PRIMA list()
        //public ViewResult List()
        //{
        //    ViewBag.Name = "DotNet, How?";
        //    //var drinks = _drinkRepository.Drinks;

        //    DrinkListViewModel vm = new DrinkListViewModel();
        //    vm.Drinks = _drinkRepository.Drinks;
        //    vm.CurrentCategory = "DrinkCategory"; 
        //    return View(vm);
        //}
        #endregion

        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Drink> drinks;

            string currentCategory = string.Empty;
            if (string.IsNullOrEmpty(category))
            {
                drinks = _drinkRepository.Drinks.OrderBy(n => n.DrinkId);
                currentCategory = "All Drinks";
            }
            else
            {
                if (string.Equals("Alcoholic", _category, StringComparison.OrdinalIgnoreCase))
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Alcoholic")).OrderBy(p => p.Name);
                else
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Non-Alcoholic")).OrderBy(p => p.Name);

                currentCategory = _category;
            }

            var drinkListViewModel = new DrinkListViewModel
            {
                Drinks = drinks,
                CurrentCategory = currentCategory
            };

            return View(drinkListViewModel);
        }

    }
}
