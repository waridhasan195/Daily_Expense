using Daily_Expense.Data;
using Daily_Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace Daily_Expense.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly DailyExpenseDbContext dailyExpenseDbContext;

        public DashBoardController(DailyExpenseDbContext dailyExpenseDbContext)
        {
            this.dailyExpenseDbContext = dailyExpenseDbContext;
        }
        public async Task<IActionResult> Index()
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Models.Transaction> selectedTransaction = await 
                dailyExpenseDbContext.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate).ToListAsync();

            int TotalIncome = selectedTransaction.Where(x => x.Category.Type == "Income").Sum(y => y.Amout);
            ViewBag.TotalIncome = TotalIncome.ToString();

            int TotalExpense = selectedTransaction.Where(x=>x.Category.Type == "Expense").Sum(y=>y.Amout);
            ViewBag.TotalExpense = TotalExpense.ToString();



            int Balance = (TotalIncome - TotalExpense);
            
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            ViewBag.DoughnutChartData = selectedTransaction
                .Where(i=>i.Category.Type=="Expense")
                .GroupBy(x=>x.Category.CategoryId)
                .Select(k=> new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                    amount = k.Sum(j=>j.Amout),
                    formattedAmount = k.Sum(j=>j.Amout).ToString("C0"),
                }).ToList();

            return View();
        }
    }
}
