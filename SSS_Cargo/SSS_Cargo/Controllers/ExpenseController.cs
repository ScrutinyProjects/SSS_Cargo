using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoBAL;
using CargoBE.Requests;
using CargoBE.Responses;

namespace SSS_Cargo.Controllers
{
    [RoutePrefix("api/expense")]
    public class ExpenseController : ApiController
    {
        #region Members

        ExpenseBal objExpenseBal = new ExpenseBal();

        #endregion

        #region APIs

        [Route("getExpenseTypes")]
        [HttpGet]
        public List<ExpenseTypeResponse> GetExpenseTypes()
        {
            List<ExpenseTypeResponse> objresponse = new List<ExpenseTypeResponse>();
            try
            {
                objresponse = objExpenseBal.GetExpenseTypes();
            }
            catch (Exception ex)
            {
               
            }
            return objresponse;
        }

        
        [Route("{loginid}/{counterid}/getexpenses")]
        [HttpGet]
        public List<ExpenseRequest> GetExpenses(string loginId, string counterId, string dateOfExpense = null)
        {
            List<ExpenseRequest> objresponse = new List<ExpenseRequest>();
            try
            {
                objresponse = objExpenseBal.GetExpenses(loginId, counterId, dateOfExpense);
            }
            catch (Exception ex)
            {

            }
            return objresponse;
        }


        [HttpPost]
        public ExpenseResponse SaveExpense(ExpenseRequest objExpenseRequest, bool isVerified)
        {
            ExpenseResponse objExpenseResponse = null;
            try
            {
                objExpenseResponse = objExpenseBal.WriteExpense(objExpenseRequest, isVerified);
            }
            catch (Exception ex)
            {
            }
            return objExpenseResponse;
        }

        [Route("{loginid}/{counterid}/lockexpenses")]
        [HttpGet]
        public SaveRespone LockExpenses(string loginId, string counterId, string dateOfExpense)
        {
            SaveRespone objSaveRespone = null;
            try
            {
                objSaveRespone = objExpenseBal.LockExpenses(loginId, counterId, dateOfExpense);
            }
            catch (Exception ex)
            {

            }
            return objSaveRespone;
        }

        [Route("{expenseid}/deleteexpense")]
        [HttpGet]
        public SaveRespone DeleteExpense(int expenseId)
        {
            SaveRespone objSaveRespone = null;
            try
            {
                objSaveRespone = objExpenseBal.DeleteExpense(expenseId);
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
