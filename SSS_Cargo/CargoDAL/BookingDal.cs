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

        public DataSet GetReceivingLocations(int counterid, int loginid)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid },
                                            new SqlParameter("@LoginId", SqlDbType.Int) { Value = loginid }
                                        };
            DataSet ds = null;

            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetReceivingLocations", sqlparams);
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

        public DataSet GetBookingsByCounterId(int counterid, int loginid)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid },
                                            new SqlParameter("@LoginId", SqlDbType.Int) { Value = loginid }
                                        };
            DataSet ds = null;

            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetBookingsByCounterId", sqlparams);
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

        public BookingCalculatedPriceResponse GetCalculatedPriceForBooking(BookingPriceCalcRequest objrequest, DataTable dtWeights)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@LoginId", SqlDbType.Int) { Value = objrequest.LoginId },
                                            new SqlParameter("@FromCounterId", SqlDbType.Int) { Value = objrequest.FromCounterId },
                                            new SqlParameter("@ToCounterId", SqlDbType.VarChar, 100) { Value = objrequest.ToCounterId },
                                            new SqlParameter("@GCType", SqlDbType.Int) { Value = objrequest.GCType },
                                            new SqlParameter("@ProductType", SqlDbType.Int) { Value = objrequest.ProductType },
                                            new SqlParameter("@BookingParcelPieces", SqlDbType.Structured) { Value = dtWeights },
                                            new SqlParameter("@TranshipmentPoints", SqlDbType.VarChar, 1000) { Value = objrequest.TranshipmentPoints },
                                            new SqlParameter("@ShipmentValue", SqlDbType.Decimal) { Value = objrequest.ShipmentValue },
                                        };

            SqlDataReader reader = null;
            BookingCalculatedPriceResponse objresponse = new BookingCalculatedPriceResponse();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetCalculatedPriceForBooking", sqlparams);

                while (reader.Read())
                {
                    objresponse.AOCCharges = (decimal)reader["AOCCharges"];
                    objresponse.BasicAmount = (decimal)reader["BasicAmount"];
                    objresponse.CollectionCharges = (decimal)reader["CollectionCharges"];
                    objresponse.DocketCharges = (decimal)reader["DocketCharges"];
                    objresponse.DoorDeliveryCharges = (decimal)reader["DoorDeliveryCharges"];
                    objresponse.HamaliCharges = (decimal)reader["HamaliCharges"];
                    objresponse.LocationDeliveryCharges = (decimal)reader["LocationDeliveryCharges"];
                    objresponse.LocationPickupCharges = (decimal)reader["LocationPickupCharges"];
                    objresponse.PickupCharges = (decimal)reader["PickupCharges"];
                    objresponse.SUPCharges = (decimal)reader["SUPCharges"];
                    objresponse.TranshipmentCharges = (decimal)reader["TranshipmentCharges"];
                    objresponse.ValueSRCharges = (decimal)reader["ValueSRCharges"];
                    objresponse.WithPassCharges = (decimal)reader["WithPassCharges"];
                    objresponse.TotalKms = (decimal)reader["TotalKms"];
                    objresponse.DiscountPercentage = (decimal)reader["DiscountPercentage"];
                    objresponse.DriverCharges = (decimal)reader["DriverCharges"];
                    objresponse.ToPayCharges = (decimal)reader["ToPayCharges"];
                    objresponse.DiscountRemarks = (string)reader["DiscountRemarks"];
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objresponse;
        }

        public BookingSaveResponse InsertBookingDetails(BookingRequest objrequest, DataTable dtWeights)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@UserLoginId", SqlDbType.Int) { Value = objrequest.UserLoginId },
                                            new SqlParameter("@FromCounterId", SqlDbType.Int) { Value = objrequest.FromCounterId },
                                            new SqlParameter("@FromCounter", SqlDbType.VarChar, 100) { Value = objrequest.FromCounter },
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
                                            new SqlParameter("@TranshipmentPoint1", SqlDbType.VarChar, 100) { Value = objrequest.TranshipmentPoint1 },
                                            new SqlParameter("@TranshipmentPoint2", SqlDbType.VarChar, 100) { Value = objrequest.TranshipmentPoint2 },
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
                                            new SqlParameter("@LocationPickupCharges", SqlDbType.Decimal) { Value = objrequest.LocationPickupCharges },
                                            new SqlParameter("@LocationDeliveryCharges", SqlDbType.Decimal) { Value = objrequest.LocationDeliveryCharges },
                                            new SqlParameter("@DoorDeliveryCharges", SqlDbType.Decimal) { Value = objrequest.DoorDeliveryCharges },
                                            new SqlParameter("@DriverCharges", SqlDbType.Decimal) { Value = objrequest.DriverCharges },
                                            new SqlParameter("@ToPayCharges", SqlDbType.Decimal) { Value = objrequest.ToPayCharges },
                                            new SqlParameter("@SubTotal", SqlDbType.Decimal) { Value = objrequest.SubTotal },
                                            new SqlParameter("@GSTCharges", SqlDbType.Decimal) { Value = objrequest.GSTCharges },
                                            new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = objrequest.TotalAmount },
                                            new SqlParameter("@DiscountAmount", SqlDbType.Decimal) { Value = objrequest.DiscountAmount },
                                            new SqlParameter("@TotalAmountAfterDiscount", SqlDbType.Decimal) { Value = objrequest.TotalAmountAfterDiscount },
                                            new SqlParameter("@RoundOffAmount", SqlDbType.Decimal) { Value = objrequest.RoundOffAmount },
                                            new SqlParameter("@GrandTotal", SqlDbType.Decimal) { Value = objrequest.GrandTotal },
                                            new SqlParameter("@TotalKms", SqlDbType.Decimal) { Value = objrequest.TotalKms },
                                            new SqlParameter("@DiscountRemarks", SqlDbType.VarChar, 500) { Value = objrequest.DiscountRemarks },
                                            new SqlParameter("@EditPriceRemarks", SqlDbType.VarChar, 500) { Value = objrequest.EditPriceRemarks },
                                            new SqlParameter("@TotalPieces", SqlDbType.Int) { Value = objrequest.TotalPieces },
                                            new SqlParameter("@TotalWeight", SqlDbType.Decimal) { Value = objrequest.TotalWeight },
                                            new SqlParameter("@WeightInfo", SqlDbType.VarChar, -1) { Value = objrequest.WeightInfo },
                                            new SqlParameter("@RouteInfo", SqlDbType.VarChar, -1) { Value = objrequest.RouteInfo },
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

                    if (objresponse.StatusId > 0)
                    {
                        objresponse.BookingSerialNumber = CommonMethods.URLKeyEncrypt((string)reader["BookingSerialNumber"]);
                        objresponse.BookingId = CommonMethods.URLKeyEncrypt(Convert.ToString((int)reader["BookingId"]));
                        objresponse.SenderMessage = (string)reader["SenderMessage"];
                        objresponse.ReceiverMessage = (string)reader["ReceiverMessage"];
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

        public void UpdateBookingBarcode(string barcodeimage, int bookingid)
        {
            SqlParameter[] sqlparams = {new SqlParameter("@BarcodeImage", SqlDbType.NVarChar, -1) { Value = barcodeimage },
                                            new SqlParameter("@BookingId", SqlDbType.Int) { Value = bookingid }
                                        };
            SqlDataReader reader = null;
            SaveRespone objresponse = new SaveRespone();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_UpdateBookingBarcode", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];
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

                        objresponse.StatusId = 1;
                        objresponse.StatusMessage = "Valid Booking Number";

                        objresponse.BookingId = (int)dr["BookingId"];
                        objresponse.GCTypeId = (int)dr["GCTypeId"];
                        objresponse.GCType = (string)dr["GCType"];
                        objresponse.MeasurementIn = (string)dr["MeasurementIn"];
                        objresponse.BookSerialNumber = (string)dr["BookSerialNumber"];
                        objresponse.TotalAmount = (decimal)dr["TotalAmount"];
                        objresponse.Route = (string)dr["Route"];
                        objresponse.TotalPieces = (int)dr["TotalPieces"];
                        objresponse.WeightInfo = (string)dr["WeightInfo"];
                    }
                    else
                    {
                        objresponse.StatusId = 0;
                        objresponse.StatusMessage = "Invalid Booking Number";
                    }
                }
                else
                {
                    objresponse.StatusId = 0;
                    objresponse.StatusMessage = "Invalid Booking Number";
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

        public SaveRespone InsertLoadingDetails(LoadingRequest objrequest)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@UserLoginId", SqlDbType.Int) { Value = objrequest.UserLoginId },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = objrequest.CounterId },
                                            new SqlParameter("@DriverName", SqlDbType.VarChar, 100) { Value = objrequest.DriverName },
                                            new SqlParameter("@DriverMobileNumber", SqlDbType.VarChar, 10) { Value = objrequest.DriverMobileNumber },
                                            new SqlParameter("@VehicleNumber", SqlDbType.VarChar, 100) { Value = objrequest.VehicleNumber },
                                            new SqlParameter("@DriverAmount", SqlDbType.Decimal) { Value = objrequest.DriverAmount },
                                            new SqlParameter("@HamaliAmount", SqlDbType.Decimal) { Value = objrequest.HamaliAmount },
                                            new SqlParameter("@LoadingDateTime", SqlDbType.VarChar, 50) { Value = objrequest.LoadingDateTime },
                                            new SqlParameter("@EstimatedDateTime", SqlDbType.VarChar, 50) { Value = objrequest.EstimatedDateTime },
                                            new SqlParameter("@Remarks", SqlDbType.VarChar, 500) { Value = objrequest.Remarks },
                                            new SqlParameter("@BookingIds", SqlDbType.VarChar, -1) { Value = objrequest.BookingIds }
                                        };
            SqlDataReader reader = null;
            SaveRespone objresponse = new SaveRespone();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_InsertLoadingDetails", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];
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

        public DataSet GetMastersForToBeReceive(int counterid, int loginid)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid },
                                            new SqlParameter("@LoginId", SqlDbType.Int) { Value = loginid }
                                        };
            DataSet ds = null;

            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetMastersForToBeReceive", sqlparams);
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

        public SaveRespone InsertToBeReceiveDetails(TobereceiveRequest objrequest)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@UserLoginId", SqlDbType.Int) { Value = objrequest.UserLoginId },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = objrequest.CounterId },
                                            new SqlParameter("@GCBookingNumber", SqlDbType.VarChar, 100) { Value = objrequest.GCBookingNumber },
                                            new SqlParameter("@GCType", SqlDbType.Int) { Value = objrequest.GCType },
                                            new SqlParameter("@FromCounter", SqlDbType.VarChar, 100) { Value = objrequest.FromCounter },
                                            new SqlParameter("@NumberOfPieces", SqlDbType.Int) { Value = objrequest.NumberOfPieces },
                                            new SqlParameter("@DriverName", SqlDbType.VarChar, 100) { Value = objrequest.DriverName },
                                            new SqlParameter("@DriverMobileNumber", SqlDbType.VarChar, 10) { Value = objrequest.DriverMobileNumber },
                                            new SqlParameter("@EstimatedDateTime", SqlDbType.VarChar, 50) { Value = objrequest.EstimatedDateTime },
                                            new SqlParameter("@Remarks", SqlDbType.VarChar, 500) { Value = objrequest.Remarks },
                                            new SqlParameter("@InformationFromId", SqlDbType.Int) { Value = objrequest.InformationFromId }
                                        };
            SqlDataReader reader = null;
            SaveRespone objresponse = new SaveRespone();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_InsertToBeReceiveDetails", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];
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

        public DataSet GetMastersForReceiving(int counterid, int loginid)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid },
                                            new SqlParameter("@LoginId", SqlDbType.Int) { Value = loginid }
                                        };
            DataSet ds = null;

            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetMastersForReceiving", sqlparams);
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

        public ToBereceiveDetailsByBookingNumber GetToBeReceivedDetailsByBookingNumber(string bookingnumber, int counterid)
        {
            SqlParameter[] sqlparams = {new SqlParameter("@BookingNumber", SqlDbType.VarChar, 20) { Value = bookingnumber },
                                           new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid }
                                        };
            SqlDataReader reader = null;
            ToBereceiveDetailsByBookingNumber objresponse = new ToBereceiveDetailsByBookingNumber();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetToBeReceivedDetailsByBookingNumber", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];

                    if (objresponse.StatusId == 1)
                    {
                        objresponse.BookingId = (int)reader["BookingId"];
                        objresponse.ToBeReceiveId = (int)reader["ToBeReceiveId"];
                        objresponse.DriverName = (string)reader["DriverName"];
                        objresponse.GCType = (string)reader["GCType"];
                        objresponse.GCTypeId = (int)reader["GCTypeId"];
                        objresponse.MeasurementIn = (string)reader["MeasurementIn"];
                        objresponse.DriverNumber = (string)reader["DriverNumber"];
                        objresponse.FromCounterName = (string)reader["FromCounterName"];
                        objresponse.EstimatedDateTime = (string)reader["EstimatedDateTime"];
                        objresponse.GCBookingNumber = (string)reader["GCBookingNumber"];
                        objresponse.NumberOfPieces = (int)reader["NumberOfPieces"];
                        objresponse.Remarks = (string)reader["Remarks"];
                        objresponse.TotalWeight = (decimal)reader["TotalWeight"];
                        objresponse.CollectionCharges = (decimal)reader["CollectionCharges"];
                    }                    
                    else if (objresponse.StatusId == 2)
                    {
                        objresponse.BookingId = (int)reader["BookingId"];
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

        public SaveRespone InsertReceivingDetails(ReceivingRequest objrequest)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@UserLoginId", SqlDbType.Int) { Value = objrequest.UserLoginId },
                                            new SqlParameter("@ToBeReceiveId", SqlDbType.Int) { Value = objrequest.ToBeReceiveId },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = objrequest.CounterId },
                                            new SqlParameter("@BookingId", SqlDbType.Int) { Value = objrequest.BookingId },
                                            new SqlParameter("@GCBookingNumber", SqlDbType.VarChar, 100) { Value = objrequest.GCBookingNumber },
                                            new SqlParameter("@GCType", SqlDbType.Int) { Value = objrequest.GCType },
                                            new SqlParameter("@FromCounter", SqlDbType.VarChar, 100) { Value = objrequest.FromCounter },
                                            new SqlParameter("@ReceivingType", SqlDbType.Int) { Value = objrequest.ReceivingType },
                                            new SqlParameter("@NumberOfPieces", SqlDbType.Int) { Value = objrequest.NumberOfPieces },
                                            new SqlParameter("@VehicleNumber", SqlDbType.VarChar, 100) { Value = objrequest.VehicleNumber },
                                            new SqlParameter("@DriverName", SqlDbType.VarChar, 100) { Value = objrequest.DriverName },
                                            new SqlParameter("@DriverMobileNumber", SqlDbType.VarChar, 10) { Value = objrequest.DriverMobileNumber },
                                            new SqlParameter("@HamaliCharges", SqlDbType.Decimal) { Value = objrequest.HamaliCharges },
                                            new SqlParameter("@TranshipmentCharges", SqlDbType.Decimal) { Value = objrequest.TranshipmentCharges },
                                            new SqlParameter("@DeliveryToName", SqlDbType.VarChar, 100) { Value = objrequest.DeliveryToName },
                                            new SqlParameter("@DeliveryToNumber", SqlDbType.VarChar, 10) { Value = objrequest.DeliveryToNumber },
                                            new SqlParameter("@Remarks", SqlDbType.VarChar, 500) { Value = objrequest.Remarks },
                                            new SqlParameter("@TotalWeight", SqlDbType.Decimal) { Value = objrequest.TotalWeight },
                                            new SqlParameter("@BillAmount", SqlDbType.Decimal) { Value = objrequest.BillAmount }
                                        };
            SqlDataReader reader = null;
            SaveRespone objresponse = new SaveRespone();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_InsertReceivingDetails", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];
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

        public BookingDetailsForPrintResponse GetBookingDetailsToPrintByBookingId(int bookingid)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@BookingId", SqlDbType.Int) { Value = bookingid }
                                        };

            SqlDataReader reader = null;
            BookingDetailsForPrintResponse objresponse = new BookingDetailsForPrintResponse();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetBookingDetailsToPrintByBookingId", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];

                    if (objresponse.StatusId == 1)
                    {
                        objresponse.ActualWeight = (string)reader["ActualWeight"];
                        objresponse.AOCCharges = (decimal)reader["AOCCharges"];
                        objresponse.BasicAmount = (decimal)reader["BasicAmount"];
                        objresponse.BookingDate = (string)reader["BookingDate"];
                        objresponse.BookingType = (string)reader["BookingType"];
                        objresponse.BookSerialNumber = (string)reader["BookSerialNumber"];
                        objresponse.CargoGSTIN = (string)reader["CargoGSTIN"];
                        objresponse.ChargedWeight = (string)reader["ChargedWeight"];
                        objresponse.CollectionCharges = (decimal)reader["CollectionCharges"];
                        objresponse.DiscountAmount = (decimal)reader["DiscountAmount"];
                        objresponse.DoorDeliveryCharges = (decimal)reader["DoorDeliveryCharges"];
                        objresponse.FromCounterGST = (string)reader["FromCounterGST"];
                        objresponse.FromCounterName = (string)reader["FromCounterName"];
                        objresponse.FromCounterPhoneNumber = (string)reader["FromCounterPhoneNumber"];
                        objresponse.GCType = (string)reader["GCType"];
                        objresponse.GrandTotal = (decimal)reader["GrandTotal"];
                        objresponse.GSTAmount = (decimal)reader["GSTAmount"];
                        objresponse.HamaliCharges = (decimal)reader["HamaliCharges"];
                        objresponse.LocationDeliveryCharges = (decimal)reader["LocationDeliveryCharges"];
                        objresponse.PaymentType = (string)reader["PaymentType"];
                        objresponse.PickupCharges = (decimal)reader["PickupCharges"];
                        objresponse.ProductType = (string)reader["ProductType"];
                        objresponse.ReceiverMobileNumber = (string)reader["ReceiverMobileNumber"];
                        objresponse.ReceiverName = (string)reader["ReceiverName"];
                        objresponse.RoundOffAmount = (decimal)reader["RoundOffAmount"];
                        objresponse.RouteInfo = (string)reader["RouteInfo"];
                        objresponse.SenderMobileNumber = (string)reader["SenderMobileNumber"];
                        objresponse.SenderName = (string)reader["SenderName"];
                        objresponse.ShipmentDescription = (string)reader["ShipmentDescription"];
                        objresponse.ShipmentValue = (decimal)reader["ShipmentValue"];
                        objresponse.SubTotal = (decimal)reader["SubTotal"];
                        objresponse.SurCharges = (decimal)reader["SurCharges"];
                        objresponse.ToCounterGST = (string)reader["ToCounterGST"];
                        objresponse.ToCounterName = (string)reader["ToCounterName"];
                        objresponse.ToCounterPhoneNumber = (string)reader["ToCounterPhoneNumber"];
                        objresponse.TotalPieces = (int)reader["TotalPieces"];
                        objresponse.TranshipmentCharges = (decimal)reader["TranshipmentCharges"];
                        objresponse.ValueSurCharges = (decimal)reader["ValueSurCharges"];
                        objresponse.WithPassCharges = (decimal)reader["WithPassCharges"]; 
                        objresponse.barcodeImage = (string)reader["barcodeImage"];
                        objresponse.DriverCharges = (decimal)reader["DriverCharges"];
                        objresponse.ToPayCharges = (decimal)reader["ToPayCharges"];
                        objresponse.GSTPercentage = (decimal)reader["GSTPercentage"];
                        objresponse.LRCharges = (decimal)reader["LRCharges"];
                        objresponse.DeliveryCharges = (decimal)reader["DeliveryCharges"]; 
                        objresponse.BookedBy = (string)reader["BookedBy"];
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objresponse;
        }

        public DataSet GetMastersForDelivery(int counterid, int loginid)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid },
                                            new SqlParameter("@LoginId", SqlDbType.Int) { Value = loginid }
                                        };
            DataSet ds = null;

            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetMastersForDelivery", sqlparams);
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

        public ReceiveDetailsByBookingNumber GetReceivingDetailsByBookingNumber(string bookingnumber, int counterid)
        {
            SqlParameter[] sqlparams = {new SqlParameter("@BookingNumber", SqlDbType.VarChar, 20) { Value = bookingnumber },
                                           new SqlParameter("@CounterId", SqlDbType.Int) { Value = counterid }
                                        };
            SqlDataReader reader = null;
            ReceiveDetailsByBookingNumber objresponse = new ReceiveDetailsByBookingNumber();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetReceivingDetailsByBookingNumber", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];

                    if (objresponse.StatusId == 1)
                    {
                        objresponse.BookingId = (int)reader["BookingId"];
                        objresponse.ReceivingId = (int)reader["ReceivingId"];
                        objresponse.GCTypeId = (int)reader["GCTypeId"];
                        objresponse.GCType = (string)reader["GCType"];
                        objresponse.FromCounterName = (string)reader["FromCounterName"];
                        objresponse.MeasurementIn = (string)reader["MeasurementIn"];
                        objresponse.GCBookingNumber = (string)reader["GCBookingNumber"];
                        objresponse.NumberOfPieces = (int)reader["NumberOfPieces"];
                        objresponse.DriverName = (string)reader["DriverName"];
                        objresponse.DriverNumber = (string)reader["DriverNumber"];
                        objresponse.VehicleNumber = (string)reader["VehicleNumber"];
                        objresponse.Remarks = (string)reader["Remarks"];
                        objresponse.TotalWeight = (decimal)reader["TotalWeight"];
                        objresponse.DeliveryToName = (string)reader["DeliveryToName"];
                        objresponse.DeliveryToNumber = (string)reader["DeliveryToNumber"];
                        objresponse.DeliveryCharges = (decimal)reader["DeliveryCharges"];
                        objresponse.DemoCharges = (decimal)reader["DemoCharges"];
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

        public DeliverySaveResponse InsertDeliveryDetails(DeliveryRequest objrequest)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@UserLoginId", SqlDbType.Int) { Value = objrequest.UserLoginId },
                                            new SqlParameter("@ReceivingId", SqlDbType.Int) { Value = objrequest.ReceivingId },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = objrequest.CounterId },
                                            new SqlParameter("@BookingId", SqlDbType.Int) { Value = objrequest.BookingId },
                                            new SqlParameter("@GCBookingNumber", SqlDbType.VarChar, 100) { Value = objrequest.GCBookingNumber },
                                            new SqlParameter("@GCType", SqlDbType.Int) { Value = objrequest.GCType },
                                            new SqlParameter("@DeliveryCharges", SqlDbType.Decimal) { Value = objrequest.DeliveryCharges },
                                            new SqlParameter("@DemoCharges", SqlDbType.Decimal) { Value = objrequest.DemoCharges },
                                            new SqlParameter("@Remarks", SqlDbType.VarChar, 500) { Value = objrequest.Remarks },
                                            new SqlParameter("@NameOfDeliveryPerson", SqlDbType.VarChar, 100) { Value = objrequest.DeliveryTo },
                                            new SqlParameter("@DeliveryPhoneNumber", SqlDbType.VarChar, 20) { Value = objrequest.DeliveryPhoneNumber },
                                            new SqlParameter("@BillAmount", SqlDbType.Decimal) { Value = objrequest.BillAmount }
                                        };
            SqlDataReader reader = null;
            DeliverySaveResponse objresponse = new DeliverySaveResponse();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_InsertDeliveryDetails", sqlparams);

                while (reader.Read())
                {
                    objresponse.StatusId = (int)reader["StatusId"];
                    objresponse.StatusMessage = (string)reader["StatusMessage"];
                    objresponse.DeliveryId =Convert.ToString((int)reader["DeliveryId"]);
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
        public DeliveryDetailsForPrintResponse GetDeliveryDetailsToPrintByDeliveryId(int deliveryId)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@DeliveryId", SqlDbType.Int) { Value = deliveryId }
                                        };

            SqlDataReader reader = null;
            DeliveryDetailsForPrintResponse objresponse = new DeliveryDetailsForPrintResponse();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetDeliveryDetailsToPrintByDeliveryId", sqlparams);
                while (reader.Read())
                {
                    objresponse.GCBookingNumber = (string)reader["GCBookingNumber"];
                    objresponse.TotalPieces = (int)reader["TotalPieces"];
                    objresponse.TotalWeight = (decimal)reader["TotalWeight"];
                    objresponse.PaymentType = (string)reader["PaymentType"];
                    objresponse.BillAmount = (decimal)reader["BillAmount"];
                    objresponse.DeliveryCharges = (decimal)reader["DeliveryCharges"];
                    objresponse.DemoCharges = (decimal)reader["DemoCharges"];
                    objresponse.Remarks = (string)reader["Remarks"];
                    objresponse.CounterName = (string)reader["CounterName"];
                    if (objresponse.PaymentType.ToString().ToLower() == "topay")
                    {
                        objresponse.TotalAmount = objresponse.BillAmount + objresponse.DeliveryCharges + objresponse.DemoCharges;
                    }
                    else
                    {
                        objresponse.TotalAmount = objresponse.DeliveryCharges + objresponse.DemoCharges;
                    }
                }

            }
            catch (Exception ex)
            {

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
