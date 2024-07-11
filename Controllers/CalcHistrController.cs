using Microsoft.AspNetCore.Mvc;
using MVCAdamBalej.Services;

namespace MVCAdamBalej.Controllers
{
    public class CalcHistrController : Controller
    {
        private readonly AppDbContext context;

        public CalcHistrController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var calcDatas = context.CalcDatas.OrderByDescending(c => c.Id).ToList();
            return View(calcDatas);
        }
    }
}
