﻿using CargoBE.Requests;
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
    public class BookingDal
    {
        #region Members

        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["SqlCon"]);

        #endregion

        #region Methods

        public DataSet GetMastersForBooking(int counterid, int loginid)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid },
                                            new SqlParameter("@LoginId", SqlDbType.Int) { Value = loginid }
                                        };
            DataSet ds = null;

            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetMastersForBooking", sqlparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return ds;
        }

        public BookingSaveResponse InsertBookingDetails(BookingRequest objrequest, DataTable dtWeights)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@UserLoginId", SqlDbType.Int) { Value = objrequest.UserLoginId },
                                            new SqlParameter("@FromCounterId", SqlDbType.Int) { Value = objrequest.FromCounterId },
                                            new SqlParameter("@ToCounter", SqlDbType.VarChar, 100) { Value = objrequest.ToCounter },
                                            new SqlParameter("@GCTypeId", SqlDbType.Int) { Value = objrequest.GCTypeId },
                                            new SqlParameter("@BookingTypeId", SqlDbType.Int) { Value = objrequest.BookingTypeId },
                                            new SqlParameter("@ProductTypeId", SqlDbType.Int) { Value = objrequest.ProductTypeId },
                                            new SqlParameter("@BookId", SqlDbType.Int) { Value = objrequest.BookId },
                                            new SqlParameter("@SenderId", SqlDbType.Int) { Value = objrequest.SenderId },
                                            new SqlParameter("@SenderName", SqlDbType.VarChar, 100) { Value = objrequest.SenderName },
                                            new SqlParameter("@SenderEmailId", SqlDbType.VarChar, 100) { Value = objrequest.SenderEmailId },
                                            new SqlParameter("@SenderMobileNumber", SqlDbType.VarChar, 10) { Value = objrequest.SenderMobileNumber },
                                            new SqlParameter("@SenderAddress", SqlDbType.VarChar, 500) { Value = objrequest.SenderAddress },
                                            new SqlParameter("@ReceiverId", SqlDbType.Int) { Value = objrequest.ReceiverId },
                                            new SqlParameter("@ReceiverName", SqlDbType.VarChar, 100) { Value = objrequest.ReceiverName },
                                            new SqlParameter("@ReceiverEmailId", SqlDbType.VarChar, 100) { Value = objrequest.ReceiverEmailId },
                                            new SqlParameter("@ReceiverMobileNumber", SqlDbType.VarChar, 10) { Value = objrequest.ReceiverMobileNumber },
                                            new SqlParameter("@ReceiverAddress", SqlDbType.VarChar, 500) { Value = objrequest.ReceiverAddress },
                                            new SqlParameter("@BookingParcelPieces", SqlDbType.Structured) { Value = dtWeights },
                                            new SqlParameter("@TranshipmentPoints", SqlDbType.VarChar, 1000) { Value = objrequest.TranshipmentPoints },
                                            new SqlParameter("@ShipmentValue", SqlDbType.Decimal) { Value = objrequest.ShipmentValue },
                                            new SqlParameter("@ShipmentDescription", SqlDbType.VarChar, 500) { Value = objrequest.ShipmentDescription },
                                            new SqlParameter("@BasicAmount", SqlDbType.Decimal) { Value = objrequest.BasicAmount },
                                            new SqlParameter("@SUPCharges", SqlDbType.Decimal) { Value = objrequest.SUPCharges },
                                            new SqlParameter("@WithPASSCharges", SqlDbType.Decimal) { Value = objrequest.WithPASSCharges },
                                            new SqlParameter("@DocketCharges", SqlDbType.Decimal) { Value = objrequest.DocketCharges },
                                            new SqlParameter("@ValueSCCharges", SqlDbType.Decimal) { Value = objrequest.ValueSCCharges },
                                            new SqlParameter("@CollectionCharges", SqlDbType.Decimal) { Value = objrequest.CollectionCharges },
                                            new SqlParameter("@HamaliCharges", SqlDbType.Decimal) { Value = objrequest.HamaliCharges },
                                            new SqlParameter("@AOCCharges", SqlDbType.Decimal) { Value = objrequest.AOCCharges },
                                            new SqlParameter("@TranshipmentCharges", SqlDbType.Decimal) { Value = objrequest.TranshipmentCharges },
                                            new SqlParameter("@PickupCharges", SqlDbType.Decimal) { Value = objrequest.PickupCharges },
                                            new SqlParameter("@GSTCharges", SqlDbType.Decimal) { Value = objrequest.GSTCharges },
                                            new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = objrequest.TotalAmount },
                                            new SqlParameter("@TotalKms", SqlDbType.Decimal) { Value = objrequest.TotalKms },
                                        };
            SqlDataReader reader = null;
            BookingSaveResponse objresponse = new BookingSaveResponse();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_InsertBookingDetails", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];
                    objresponse.BookingSerialNumber = (string)reader["BookingSerialNumber"];
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objresponse;
        }

        public CustomerDetailsResponse GetCustomerDetailsByMobileNumber(string mobilenumber)
        {
            SqlParameter[] sqlparams = {new SqlParameter("@MobileNumber", SqlDbType.VarChar, 10) { Value = mobilenumber }
                                        };
            SqlDataReader reader = null;
            CustomerDetailsResponse objresponse = new CustomerDetailsResponse();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetCustomerDetailsByMobileNumber", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];

                    if (objresponse.StatusId == 1)
                    {
                        objresponse.Address = (string)reader["Address"];
                        objresponse.CustomerId = (int)reader["CustomerId"];
                        objresponse.CustomerName = (string)reader["CustomerName"];
                        objresponse.MobileNumber = (string)reader["MobileNumber"];
                    }
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objresponse;
        }

        public BookingDetailsByBookingNumber GetBookingDetailsByBookingNumber(string bookingnumber, int counterid)
        {
            SqlParameter[] sqlparams = {new SqlParameter("@BookingNumber", SqlDbType.VarChar, 20) { Value = bookingnumber },
                                           new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid }
                                        };
            DataSet ds = null;
            BookingDetailsByBookingNumber objresponse = new BookingDetailsByBookingNumber();

            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetBookingDetailsByBookingNumber", sqlparams);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        objresponse.StatusId = (int)dr["StatusId"];
                        objresponse.StatusMessage = (string)dr["StatusMessage"];

                        if (objresponse.StatusId == 1)
                        {
                            objresponse.BookingId = (int)dr["BookingId"];
                            objresponse.BookSerialNumber = (string)dr["BookSerialNumber"];
                            objresponse.GCType = (string)dr["GCType"];
                            objresponse.GCTypeId = (int)dr["GCTypeId"];
                            objresponse.MeasurementIn = (string)dr["MeasurementIn"];
                            objresponse.TotalAmount = (decimal)dr["TotalAmount"];

                            if (ds.Tables.Count > 1)
                            {
                                objresponse.BookingParcels = ds.Tables[1].AsEnumerable().
                                          Select(x => new BookingParcelDetails
                                          {
                                              ActualWeight = x.Field<decimal>("ActualWeight"),
                                              BookingParcelId = x.Field<int>("BookingParcelId"),
                                              NumberOfPieces = x.Field<int>("NumberOfPieces"),
                                              ParcelType = x.Field<string>("ParcelType"),
                                              ParcelTypeId = x.Field<int>("ParcelTypeId"),
                                              TotalWeight = x.Field<decimal>("TotalWeight")
                                          }).ToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objresponse;
        }

        #endregion
    }
}
