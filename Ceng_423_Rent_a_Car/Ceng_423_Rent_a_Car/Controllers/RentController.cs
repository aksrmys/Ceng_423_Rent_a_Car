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
    public class RentController : Controller
    {
        private readonly IData data;
        public RentController(IData data)
        {
            this.data = data;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Rent rent)
        {
            if (!ModelState.IsValid)
                return View(rent);
            ViewBag.isSaved = data.BookingNow(rent);
            ModelState.Clear();
            return View();
        }
        [HttpGet]

        public IActionResult GetBrand() {
            var list = data.GetBrand();
            return Json(list);
        }
        [HttpGet]

        public IActionResult GetModel(string brand)
        {
            var list = data.GetModel(brand);
            return Json(list);
        }
        [HttpGet]

        public IActionResult GetDriver() 
        {
            var list = data.GetAllDrivers();
            return Json(list);
        }
    }
}

