using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApp.Models
{
    public class Country
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Region { get; set; }
        public int? Population { get; set; }
        public string? Flag { get; set; }
        public string? Capital { get; set; }

        public string? OfficialName { get; set; }
        //public IEnumerable<Picture>? Pictures { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}
