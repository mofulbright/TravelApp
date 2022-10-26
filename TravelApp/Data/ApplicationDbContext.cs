using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

namespace TravelApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CountryDb> TravelList { get; set; }
    }
}
