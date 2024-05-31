using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }
        public DbSet<Refuel> Refuels { get; set; }
    }
}
