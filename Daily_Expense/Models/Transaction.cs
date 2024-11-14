using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Daily_Expense.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int Amout { get; set; }

        [Column(TypeName = "nvarchar(700)")]
        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        //[NotMapped]
        //public int CetegoryExpenseIncome 
        //{
        //    get
        //    {
        //        return (Category.Type == "Expense", +" - " + : +" + " + Amout.ToString());
        //    }
        //}
    }
}
