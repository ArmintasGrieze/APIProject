using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace APIdemo.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<User> Users { get; set; }
    }
}