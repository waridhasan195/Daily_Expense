using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Daily_Expense.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = " Please select a category.")]
        public int CategoryId { get; set; }


        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage =" Please enter 0< amount.")]
        public int Amout { get; set; }

        [Column(TypeName = "nvarchar(700)")]
        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string Formated_Salary
        { get {
                return ((Category == null || Category.Type == "Expense") ? " - " : " + ") + Amout.ToString();
            }

        }
    }
}
