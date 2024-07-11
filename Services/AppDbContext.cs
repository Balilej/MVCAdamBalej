using MVCAdamBalej.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCAdamBalej.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CalcData> CalcDatas { get; set; }
    }
}
