using Daily_Expense.Data;
using Daily_Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
            ViewBag.Balance = Balance.ToString("C0");

            return View();
        }
    }
}
