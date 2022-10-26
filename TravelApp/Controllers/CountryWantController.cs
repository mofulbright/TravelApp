using Microsoft.AspNetCore.Mvc;
using TravelApp.Data;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class CountryWantController : Controller
    {
        private readonly ApplicationDbContext _db;
        ApiGet api = new ApiGet();
        CountryDb countryDb = new CountryDb();

        public CountryWantController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        { 
            IEnumerable<CountryDb> objCountryWantList = _db.TravelList;
            return View(objCountryWantList);
        }

        public IActionResult Add(Country country)
        {
            countryDb.Flag = country.flags.png;
            countryDb.Id = country.ccn3;
            countryDb.Name = country.name.common;
            countryDb.Region = country.region;
                
           
            try
            {
                _db.TravelList.Add(countryDb);
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

        public IActionResult Delete(CountryDb country)
        {
            
            _db.TravelList.Remove(country);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
