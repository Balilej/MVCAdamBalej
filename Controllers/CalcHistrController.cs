// Controllers/CalcHistrController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAdamBalej.Models;
using MVCAdamBalej.Services;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace MVCAdamBalej.Controllers
{
    public class CalcHistrController : Controller
    {
        private readonly AppDbContext _context;

        public CalcHistrController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var calcDatas = _context.CalcDatas.OrderByDescending(c => c.Id).Take(10).ToList();
            return View(calcDatas);
        }

        [HttpPost]
        public IActionResult Calculate(string expression)
        {
            try
            {
                // Validate the expression
                if (string.IsNullOrWhiteSpace(expression) || !IsValidExpression(expression))
                {
                    return Json(new { result = "Not calculable", history = _context.CalcDatas.OrderByDescending(c => c.Id).Take(10).ToList() });
                }

                var result = new DataTable().Compute(expression, null);

                var calcEntry = $"{expression} = {result}";

                var calcData = new CalcData
                {
                    CalcEntry = calcEntry
                };

                _context.CalcDatas.Add(calcData);
                _context.SaveChanges();

                // Fetch the newly added calcData from the database to get the actual Id
                var newCalcData = _context.CalcDatas.OrderByDescending(c => c.Id).FirstOrDefault();

                return Json(new { result = calcEntry, history = _context.CalcDatas.OrderByDescending(c => c.Id).Take(10).ToList() });
            }
            catch (Exception ex)
            {
                SendError(ex);
                return Json(new { error = ex.Message });
            }
        }

        private bool IsValidExpression(string expression) // Use regex to validate the expression
        {
            var regex = new Regex(@"^[0-9\+\-\*/\s\(\)\.]+$");
            return regex.IsMatch(expression);
        }










        private void SendError(Exception exception)
        {
            // Log errors to file 
         
        }
    }
}
