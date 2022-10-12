using Microsoft.AspNetCore.Mvc;

namespace TravelApp.Controllers
{
    public class CountryController : Controller
    {
        ApiGet api = new ApiGet();
        public IActionResult Index(string option="0")
        {
            return View(api.GetAll(option));
        }
        
        //public IActionResult OrderBy(int option)
        //{
        //    return View(api.OrderBy(api.GetAll(), option));
        //}

        public IActionResult ViewCountry(string id)
        {
            return View(api.GetOne(id));
        }

        [HttpPost]
        public IActionResult SearchCountry(string searchValue)
        {
            return View(api.Search(searchValue));
        }

        public IActionResult DoSomething(string date)
        {
            return View(date);
        }

        public IActionResult ViewPictures(string id)
        {
            return View(api.GetOne(id));
        }
        
    }
}
