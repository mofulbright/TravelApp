using Microsoft.AspNetCore.Mvc;
using TravelApp.Data;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class CountryWantController : Controller
    {
        private readonly ApplicationDbContext _db;
        ApiGet api = new ApiGet();

        public CountryWantController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        { 
            IEnumerable<Country> objCountryWantList = _db.TravelList;
            return View(objCountryWantList);
        }

        public IActionResult Add(string id)
        {
            try
            {
                _db.TravelList.Add(api.GetOne(id));
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Custom", "Country already in TravelList");
                TempData["Message"] = "Country already in TravelList";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(string id)
        {
            var obj = _db.TravelList.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.TravelList.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
