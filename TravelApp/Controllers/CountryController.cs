using Microsoft.AspNetCore.Mvc;

namespace TravelApp.Controllers
{
    public class CountryController : Controller
    {
        ApiGet api = new ApiGet();
        public IActionResult Index()
        {
            return View(api.GetAll());
        }

        public IActionResult ViewCountry(string id)
        {
            return View(api.GetOne(id));
        }
    }
}
