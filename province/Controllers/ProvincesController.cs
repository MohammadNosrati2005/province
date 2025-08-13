using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using province.Models.Entities;

namespace province.Controllers
{
    public class ProvincesController : Controller
    {

        private readonly AppDbContext _Context;

        public ProvincesController(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<IActionResult> List()
        {
            var ListProvinc = await _Context.Provinces.ToListAsync();
            return View(ListProvinc);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Province province)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(province);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(province);
        }
    }
}
