using CargoBE.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBE.Responses
{
    public class ExpenseResponse
    {
        public SaveRespone objSaveResponse { get; set; }
        public ExpenseRequest objExpenseRequest { get; set; }
    }
}
