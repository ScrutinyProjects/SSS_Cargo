using CargoBE;
using CargoBE.Requests;
using CargoBE.Responses;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoDAL
{
    public class ExpenseDal
    {
        #region Members

        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["SqlCon"]);

        #endregion

        #region Methods

        public DataSet GetExpenseTypes()
        {
            DataSet dsExpenseType = null;
            try
            {
                dsExpenseType = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetExpenseTypes");
            }
            catch (Exception ex)
            {

            }
            return dsExpenseType;
        }

        public DataSet GetExpenses(string loginId,string counterId, string dateOfExpense = null)
        {
            DataSet dsExpenses = null;
            try
            {
                
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(loginId)) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(counterId)) },
                                            new SqlParameter("@DateOfExpense", SqlDbType.Date) { Value = (string.IsNullOrEmpty(dateOfExpense) ? (Object)DBNull.Value : Convert.ToString(Convert.ToDateTime(dateOfExpense))) }
                                        };
                dsExpenses = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetExpenses", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsExpenses;
        }

        public ExpenseResponse WriteExpense(ExpenseRequest objExpenseRequest, bool isVerified)
        {
            ExpenseResponse objExpenseResponse = new ExpenseResponse();
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@ExpenseId", SqlDbType.Int) { Value = objExpenseRequest.ExpenseId },
                                            new SqlParameter("@ExpenseTypeId", SqlDbType.Int) { Value = objExpenseRequest.ExpenseTypeId },
                                            new SqlParameter("@Amount", SqlDbType.Decimal) { Value = objExpenseRequest.Amount },
                                            new SqlParameter("@ExpenseTo", SqlDbType.VarChar) { Value = objExpenseRequest.ExpenseTo },
                                            new SqlParameter("@ContactNumber", SqlDbType.VarChar) { Value = objExpenseRequest.ContactNumber },
                                            new SqlParameter("@DateOfExpense", SqlDbType.Date) { Value = objExpenseRequest.DateOfExpense },
                                            new SqlParameter("@Remarks", SqlDbType.VarChar) { Value = objExpenseRequest.Remarks },
                                            new SqlParameter("@EditRemarks", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(objExpenseRequest.EditRemarks) ? (object)DBNull.Value : objExpenseRequest.EditRemarks },
                                            new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(objExpenseRequest.CreatedBy)) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(objExpenseRequest.CounterId)) },
                                            new SqlParameter("@IsVerified", SqlDbType.Bit) { Value = isVerified },

                                        };
                SqlDataReader reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_WriteExpense", sqlparams);
                while (reader.Read())
                {
                    if (objExpenseResponse.objSaveResponse == null)
                    {
                        objExpenseResponse.objSaveResponse = new SaveRespone();
                        objExpenseResponse.objSaveResponse.StatusId = (int)reader["StatusId"];
                        if (objExpenseResponse.objSaveResponse.StatusId == 1)
                            objExpenseResponse.objSaveResponse.StatusMessage = "Expense added successfully.";
                        else if (objExpenseResponse.objSaveResponse.StatusId == 2)
                            objExpenseResponse.objSaveResponse.StatusMessage = "We found records similar to this entry.";
                        else if (objExpenseResponse.objSaveResponse.StatusId == 3)
                            objExpenseResponse.objSaveResponse.StatusMessage = "Date: " + objExpenseRequest.DateOfExpense + " expenses summary is closed, You can't enter expenses for the selected date.";
                    }
                    else if (objExpenseResponse.objSaveResponse.StatusId == 2)
                    {
                        objExpenseResponse.objExpenseRequest = new ExpenseRequest();
                        objExpenseResponse.objExpenseRequest.ExpenseId = Convert.ToInt32(reader["ExpenseId"]);
                        objExpenseResponse.objExpenseRequest.ExpenseTypeId = Convert.ToInt32(reader["ExpenseTypeId"]);
                        objExpenseResponse.objExpenseRequest.ExpenseType = Convert.ToString(reader["ExpenseType"]);
                        objExpenseResponse.objExpenseRequest.Amount = Convert.ToDecimal(reader["Amount"]);
                        objExpenseResponse.objExpenseRequest.Remarks = Convert.ToString(reader["Remarks"]);
                    }
                    reader.NextResult();
                }

            }
            catch (Exception ex)
            {
                objExpenseResponse.objSaveResponse.StatusMessage = "oops something went wrong, please try again ";
                //throw;
            }
            return objExpenseResponse;
        }

        public SaveRespone LockExpenses(string loginId, string counterId, string dateOfExpense)
        {
            SaveRespone objSaveRespone = new SaveRespone();
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(loginId)) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(counterId)) },
                                             new SqlParameter("@DateOfExpense", SqlDbType.Date) { Value =  dateOfExpense }
                                        };
                SqlDataReader reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_LockExpenses", sqlparams);
                while (reader.Read())
                {
                    objSaveRespone.StatusId = (int)reader["StatusId"];
                    if (objSaveRespone.StatusId == 1)
                        objSaveRespone.StatusMessage = "Expenses locked successfully.";
                    else
                        objSaveRespone.StatusMessage = "Expenses for this date already locked.";
                }
            }
            catch (Exception)
            {

                //  throw;
            }
            return objSaveRespone;
        }

        public SaveRespone DeleteExpense(int expenseId)
        {
            SaveRespone objSaveRespone = new SaveRespone();
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@ExpenseId", SqlDbType.Int) { Value = expenseId },
                                          
                                        };
                SqlDataReader reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_DeleteExpense", sqlparams);
                while (reader.Read())
                {
                    objSaveRespone.StatusId = (int)reader["StatusId"];
                    if (objSaveRespone.StatusId == 1)
                        objSaveRespone.StatusMessage = "Expense deleted successfully.";
                    else
                        objSaveRespone.StatusMessage = "Expense date locked";
                }
            }
            catch (Exception ex)
            {

                //  throw;
            }
            return objSaveRespone;
        }
        #endregion
    }
}
