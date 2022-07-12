using Newtonsoft.Json.Linq;
using System.Dynamic;
using TravelApp.Models;

namespace TravelApp
{
    public class ApiGet
    {
        HttpClient client = new HttpClient();
        public IEnumerable<Country> GetAll()
        {
            var endpoint = "https://restcountries.com/v3.1/all";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result);
            IEnumerable<Country> allCountries = json.Select(p => new Country
            {
                Id = (string?)p["ccn3"],
                Name = (string?)p["name"]["common"],
                Region = (string?)p["subregion"],
                Population = (int?)p["population"]
            });
            
            return allCountries.OrderBy(x => x.Name);
        }

        public Country GetOne(string id)
        {
            var endpoint = $"https://restcountries.com/v3.1/alpha/{id}";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result);

            Country singleCountry = new Country()
            {
                Id = (string?)json[0]["ccn3"],
                Name = (string?)json[0]["name"]["common"],
                Region = (string?)json[0]["subregion"],
                Population = (int?)json[0]["population"],
                Flag = (string?)json[0]["flags"]["png"],
                OfficialName = (string?)json[0]["name"]["official"],
                Capital = (string?)json[0]["capital"][0]
            };
            return singleCountry;
        }

        //public void GetPicture()
        //{
        //    var key = File.ReadAllText("appsettings.json");
        //    var apiKey = JObject.Parse(key).GetValue("PixabayAPIKey");

        //    var endpoint = $"https://pixabay.com/api/?key={apiKey}";
        //    var result = client.GetStringAsync(endpoint).Result;
        //    //dynamic myModel = new ExpandoObject();
        //}
    }
}
