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
    public class DriverController : Controller
    {
        private readonly IData data;
        public DriverController(IData _data)
        {
            data = _data;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = data.GetAllDrivers();

            return View(list);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Add(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return View(driver);
            }
            ViewBag.isSaved = data.AddDriver(driver);
            ModelState.Clear();
            return View();
        }

        public IActionResult History(int Id)
        {
            var list = data.GetDriverHistory(Id);
            return View(list);
        }
    }
}

