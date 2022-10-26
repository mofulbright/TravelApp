using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApp.Models
{
    public class CountryDb
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Region { get; set; }
        public string? Flag { get; set; }

    }
}
