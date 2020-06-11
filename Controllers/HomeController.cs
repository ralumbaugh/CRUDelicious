using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            Dish[] AllDishes = dbContext.Dishes.ToArray();
            Dish[] ReverseDishes = AllDishes.OrderByDescending(dish => dish.CreatedAt).ToArray();
            return View(ReverseDishes);
        }

        [HttpGet("new")]
        public IActionResult MakeNewDish()
        {
            return View("NewDish");
        }

        [HttpPost("NewDish")]
        public IActionResult NewDish(Dish dish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewDish");
            }
        }

        [HttpGet("delete/{dishId}")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish DishToDelete = dbContext.Dishes.SingleOrDefault(dish => dish.DishId == dishId);
            dbContext.Dishes.Remove(DishToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish DishToEdit = dbContext.Dishes.SingleOrDefault(dish => dish.DishId == dishId);
            return View(DishToEdit);
        }

        [HttpPost("UpdateDish")]
        public IActionResult UpdateDish(int dishId, Dish editedDish)
        {
            if(ModelState.IsValid)
            {
                editedDish.DishId = dishId;
                dbContext.Update(editedDish);
                dbContext.Entry(editedDish).Property("CreatedAt").IsModified = false;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditDish", editedDish);
            }
        }

        [HttpGet("{DishId}")]
        public IActionResult Dish(int DishId)
        {
            Dish CurrentDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
            return View(CurrentDish);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
