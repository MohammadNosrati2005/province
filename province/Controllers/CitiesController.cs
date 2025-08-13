using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using province.Models.Entities;

namespace province.Controllers
{
    public class CitiesController : Controller
    {
        private readonly AppDbContext _Context;

        public CitiesController(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<IActionResult> List()
        {
            ViewBag.Provinces = _Context.Provinces.ToList();
            var listCity = await _Context.Cities
                .Include(c => c.province)
                .ToListAsync();

            return View(listCity);
        }
        
        public IActionResult Create()
        {
            var provinces = _Context.Provinces.ToList();
            ViewBag.Provinces = provinces; 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(city);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            ViewBag.Provinces = _Context.Provinces.ToList();

            return View(city);
        }
        public JsonResult GetCitiesByProvinces(int provinceId)
        {
         var cities =    _Context.Cities
                .Where(c => c.ProvinceId == provinceId)
                .Select(c => new { c.Id, c.Name })
                .ToList();

            return Json(cities);

        }
      public IActionResult Filter()
        {
            ViewBag.Provinces = _Context.Provinces.ToList();
            return View();
        }
        public IActionResult Index()
        {
            ViewBag.Provinces = _Context.Provinces.ToList();
            return View();
        }

    }
}
