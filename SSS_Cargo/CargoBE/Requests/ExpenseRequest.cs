using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBE.Requests
{
    public class ExpenseRequest
    {
        public int ExpenseId { get; set; }
        public int ExpenseTypeId { get; set; }

        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseTo { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfExpense { get; set; }
        public string Remarks { get; set; }
        public string EditRemarks { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CounterId { get; set; }
    }
}
