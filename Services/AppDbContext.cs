// Services/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using MVCAdamBalej.Models;

namespace MVCAdamBalej.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CalcData> CalcDatas { get; set; }
    }
}
