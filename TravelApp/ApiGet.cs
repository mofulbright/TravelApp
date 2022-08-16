using Newtonsoft.Json.Linq;
using System.Dynamic;
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
            var json = JArray.Parse(result);
            IEnumerable<Country> countries = json.Select(p => new Country
            {
                Id = (string?)p["ccn3"],
                Name = (string?)p["name"]["common"],
                Region = (string?)p["region"],
                Flag = (string?)p["flags"]["png"],
                Population = (int?)p["population"]
            });
            
            //return allCountries.OrderBy(x => x.Name);
            switch (option)
            {
                case "0":
                    return countries.OrderBy(x => x.Id);
                    break;
                case "1":
                    return countries.OrderByDescending(x => x.Id);
                    break;
                case "2":
                    return countries.OrderBy(x => x.Population);
                    break;
                case "3":
                    return countries.OrderByDescending(x => x.Population);
                    break;
                case "4":
                    return countries.OrderBy(x => x.Name);
                    break;
                case "5":
                    return countries.OrderBy(x => x.Region);
                    break;
                default: return countries;
                    break;
            }
            //return countries;
        }

        public IEnumerable<Country> OrderBy(IEnumerable<Country> countries, int option)
        {
            switch (option)
            {
                case 0:
                    countries.OrderBy(x => x.Id);
                    break;
                case 1:
                    countries.OrderBy(x => x.Population);
                    break;
                case 2:
                    countries.OrderBy(x => x.Name);
                    break;
            }
            return countries;
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
                Capital = (string?)json[0]["capital"][0],
                Pictures = GetPictureAsync((string?)json[0]["name"]["common"]).Result
            };
            return singleCountry;
        }

        public IEnumerable<Country> Search(string name)
        {
            var endpoint = $"https://restcountries.com/v3.1/name/{name}";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result);
            IEnumerable<Country> allCountries = json.Select(p => new Country
            {
                Id = (string?)p["ccn3"],
                Name = (string?)p["name"]["common"],
                Region = (string?)p["subregion"],
                Flag = (string?)p["flags"]["png"],
                Population = (int?)p["population"]               
            });
            return allCountries.OrderBy(x => x.Name);
        }

        public static async Task<IEnumerable<Picture>> GetPictureAsync(string searchString)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://bing-image-search1.p.rapidapi.com/images/search?q={searchString}"),
                Headers =
    {
        { "X-RapidAPI-Key", "e3896483ebmsh27a55a3d1d8712bp1e7de6jsn1afe90ad39e9" },
        { "X-RapidAPI-Host", "bing-image-search1.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body)["value"].ToArray();
                IEnumerable<Picture> pictures = json.Select(p => new Picture
                {
                    contentUrl = (string)p["contentUrl"],
                    height = (int)p["height"],
                    width = (int)p["width"],
                    pageUrl = (string)p["hostPageUrl"]
                });
                return pictures;
            }
        }
    }
}
