using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text.Json;
using TravelApp.Models;

namespace TravelApp
{
    public class ApiGet
    {
        HttpClient client = new HttpClient();
        public IEnumerable<Country> GetAll(string option)
        {
            var endpoint = "https://restcountries.com/v3.1/all";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result).ToString();

            IEnumerable<Country> countries = JsonSerializer.Deserialize<List<Country>>(json);

            switch (option)
            {
                case "0":
                    return countries.OrderBy(x => x.ccn3);
                    break;
                case "1":
                    return countries.OrderByDescending(x => x.ccn3);
                    break;
                case "2":
                    return countries.OrderBy(x => x.population);
                    break;
                case "3":
                    return countries.OrderByDescending(x => x.population);
                    break;
                case "4":
                    return countries.OrderBy(x => x.name.common);
                    break;
                case "5":
                    return countries.OrderBy(x => x.region);
                    break;
                default: return countries;
                    break;
            }
        }

        public Country GetOne(string id)
        {
            var endpoint = $"https://restcountries.com/v3.1/alpha/{id}";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result).ToString();
            var country = JsonSerializer.Deserialize<List<Country>>(json);
            country[0].pictures = GetPictures(country[0].name.common);
            return country[0];
        }

        public IEnumerable<Country> Search(string name)
        {
            var endpoint = $"https://restcountries.com/v3.1/name/{name}";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result).ToString();
            var country = JsonSerializer.Deserialize<List<Country>>(json);
            return country.OrderBy(x => x.name.common);
        }

        public static IEnumerable<Picture> GetPictures(string searchString)
        {
            var client = new HttpClient();
            var sett = File.ReadAllText("appsettings.json");
            string key = JObject.Parse(sett)["ConnectionStrings"]["RapidAPIKey"].ToString();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://bing-image-search1.p.rapidapi.com/images/search?q={searchString}"),
                Headers =
    {
        { "X-RapidAPI-Key", key },
        { "X-RapidAPI-Host", "bing-image-search1.p.rapidapi.com" },
    },
            };
            using (var response = client.SendAsync(request).Result)
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(body)["value"].ToArray();
                IEnumerable<Picture> pictures = json.Select(p => new Picture
                {
                    contentUrl = (string)p["contentUrl"],
                    height = (int)p["height"],
                    width = (int)p["width"],
                    pageUrl = (string)p["hostPageUrl"],
                    description = (string)p["name"]

                });
                return pictures;
            }
        }
    }
}
