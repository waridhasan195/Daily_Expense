using Daily_Expense.Models;
using Microsoft.EntityFrameworkCore;

namespace Daily_Expense.Data
{
    public class DailyExpenseDbContext : DbContext
    {
        public DailyExpenseDbContext(DbContextOptions<DailyExpenseDbContext> options) 
            : base(options) 
        { 

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
