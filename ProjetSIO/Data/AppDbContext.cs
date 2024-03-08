using Microsoft.EntityFrameworkCore;
using ProjetSIO.Models;

namespace ProjetSIO.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ReserveEau> ReservesEau { get; set; }
    }
}
