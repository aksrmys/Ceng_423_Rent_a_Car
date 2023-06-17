using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ceng_423_Rent_a_Car.Models;
using Ceng_423_Rent_a_Car.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ceng_423_Rent_a_Car.Controllers
{
    public class CarController : Controller
    {
        private readonly IData data;
        public CarController (IData _data)
        {
            data = _data;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = data.GetAllCars();
            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Add(Car newcar)
        {
            if (!ModelState.IsValid)
            {
                return View(newcar);
            }
            bool isSaved = data.AddNewCar(newcar);
            ViewBag.isSaved = isSaved;
            ModelState.Clear();
            return View();
        }
    }
}

