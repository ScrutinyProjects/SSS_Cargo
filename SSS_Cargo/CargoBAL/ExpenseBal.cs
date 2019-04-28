using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoBE.Requests;
using CargoBE.Responses;
using CargoDAL;

namespace CargoBAL
{
    public class ExpenseBal
    {
        #region Members

        ExpenseDal objExpenseDal = new ExpenseDal();

        #endregion

        #region Methods
        public List<ExpenseTypeResponse> GetExpenseTypes()
        {
            List<ExpenseTypeResponse> lstExpenseTypes = null;

            try
            {
                DataSet ds = new DataSet();
                ds = objExpenseDal.GetExpenseTypes();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        lstExpenseTypes = ds.Tables[0].AsEnumerable().
                                           Select(x => new ExpenseTypeResponse
                                           {
                                               ExpenseTypeId = x.Field<int>("ExpenseTypeId"),
                                               ExpenseType = x.Field<string>("ExpenseType")
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstExpenseTypes;
        }

        public List<ExpenseRequest> GetExpenses(string loginId, string counterId, string dateOfExpense = null)
        {
            List<ExpenseRequest> lstExpenseRequest = null;
            try
            {
                DataSet ds = objExpenseDal.GetExpenses(loginId, counterId, dateOfExpense);
                if (ds != null)
                {

                    if (ds.Tables.Count > 0)
                    {
                        lstExpenseRequest = ds.Tables[0].AsEnumerable().
                                           Select(x => new ExpenseRequest
                                           {
                                               ExpenseId = x.Field<int>("ExpenseId"),
                                               ExpenseTypeId = x.Field<int>("ExpenseTypeId"),
                                               ExpenseType = x.Field<string>("ExpenseType"),
                                               Amount = x.Field<decimal>("Amount"),
                                               ExpenseTo = x.Field<string>("ExpenseTo"),
                                               ContactNumber = x.Field<string>("ContactNumber"),
                                               DateOfExpense = x.Field<DateTime>("DateOfExpense"),
                                               Remarks = x.Field<string>("Remarks"),
                                               EditRemarks = x.Field<string>("EditRemarks"),
                                               CreatedDate = x.Field<DateTime>("CreatedDate")
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
            return lstExpenseRequest;
        }
        public ExpenseResponse WriteExpense(ExpenseRequest objExpenseRequest,bool isVerified)
        {
            ExpenseResponse objExpenseResponse = null;
            try
            {
                objExpenseResponse = objExpenseDal.WriteExpense(objExpenseRequest, isVerified);
            }
            catch (Exception)
            {

                throw;
            }
            return objExpenseResponse;

        }
        public SaveRespone LockExpenses(string loginId,string counterId, string dateOfExpense)
        {
            SaveRespone objSaveRespone = null;
            try
            {
                objSaveRespone = objExpenseDal.LockExpenses(loginId, counterId, dateOfExpense);
            }
            catch (Exception)
            {

                throw;
            }
            return objSaveRespone;
        }
        public SaveRespone DeleteExpense(int expenseId)
        {
            SaveRespone objSaveRespone = null;
            try
            {
                objSaveRespone = objExpenseDal.DeleteExpense(expenseId);
            }
            catch (Exception)
            {

                throw;
            }
            return objSaveRespone;
        }
        #endregion
    }
}
