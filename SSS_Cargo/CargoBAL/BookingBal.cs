﻿using CargoBE;
using CargoBE.Requests;
using CargoBE.Responses;
using CargoDAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBAL
{
    public class BookingBal
    {
        #region Members

        BookingDal objBookingDal = new BookingDal();

        #endregion

        #region Methods

        public BookingMastersResponse GetMastersForBooking(JObject input)
        {
            BookingMastersResponse objresponse = new BookingMastersResponse();

            try
            {
                DataSet ds = new DataSet();

                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                int loginid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));

                ds = objBookingDal.GetMastersForBooking(counterid, loginid);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        var gctypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 1).
                                           Select(x => new GCTypesResponse
                                           {
                                               GCTypeId = x.Field<int>("Id"),
                                               GCType = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.GCTypes = gctypes;

                        var counters = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 2).
                                           Select(x => new CounterMastersResponse
                                           {
                                               CounterId = x.Field<int>("Id"),
                                               CounterName = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.Counters = counters;

                        var parceltypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 3).
                                           Select(x => new ParcelTypesResponse
                                           {
                                               ParcelTypeId = x.Field<int>("Id"),
                                               ParcelType = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.ParcelTypes = parceltypes;

                        var producttypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 4).
                                           Select(x => new ProductTypesResponse
                                           {
                                               ProductTypeId = x.Field<int>("Id"),
                                               ProductType = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.ProductTypes = producttypes;

                        var books = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 5).
                                           Select(x => new BooksMasterResponse
                                           {
                                               BookId = x.Field<int>("Id"),
                                               BookName = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.Books = books;

                        var customers = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 6).
                                           Select(x => new CustomersMasterResponse
                                           {
                                               CustomerId = x.Field<int>("Id"),
                                               MobileNumber = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.Customers = customers;

                        var paidtypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 7).
                                           Select(x => new PaidTypesMasterResponse
                                           {
                                               PaidTypeId = x.Field<int>("Id"),
                                               PaidTypeName = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.PaidTypes = paidtypes;

                        objresponse.StatusId = 1;
                        objresponse.StatusMessage = "Valid";
                    }
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public List<CounterMastersResponse> GetReceivingLocations(JObject input)
        {
            List<CounterMastersResponse> objresponse = new List<CounterMastersResponse>();

            try
            {
                DataSet ds = new DataSet();

                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                int loginid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));

                ds = objBookingDal.GetReceivingLocations(counterid, loginid);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        objresponse = ds.Tables[0].AsEnumerable().
                                           Select(x => new CounterMastersResponse
                                           {
                                               CounterId = x.Field<int>("Id"),
                                               CounterName = x.Field<string>("Name")
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
            return objresponse;
        }

        public List<BookingsListByCounter> GetBookingsByCounterId(JObject input)
        {
            List<BookingsListByCounter> objresponse = new List<BookingsListByCounter>();

            try
            {
                DataSet ds = new DataSet();

                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                int loginid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));

                ds = objBookingDal.GetBookingsByCounterId(counterid, loginid);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        objresponse = ds.Tables[0].AsEnumerable().
                                           Select(x => new BookingsListByCounter
                                           {
                                               BookingId = x.Field<int>("BookingId"),
                                               BookSerialNumber = x.Field<string>("BookSerialNumber"),
                                               BookingDate = x.Field<string>("BookingDate"),
                                               TotalWeight = x.Field<string>("TotalWeight"),
                                               GCType = x.Field<string>("GCType"),
                                               PaymentType = x.Field<string>("PaymentType"),
                                               RouteInfo = x.Field<string>("RouteInfo"),
                                               SenderMobileNumber = x.Field<string>("SenderMobileNumber"),
                                               SenderName = x.Field<string>("SenderName"),
                                               TotalPieces = x.Field<int>("TotalPieces"),
                                               TotalAmount = x.Field<decimal>("TotalAmount"),
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objresponse;
        }

        public BookingCalculatedPriceResponse GetCalculatedPriceForBooking(JObject input)
        {
            BookingCalculatedPriceResponse objresponse = new BookingCalculatedPriceResponse();

            try
            {
                BookingPriceCalcRequest objrequest = new BookingPriceCalcRequest();
                
                objrequest.FromCounterId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                objrequest.GCType = Convert.ToInt32(input["GCTypeId"]);
                objrequest.ProductType = Convert.ToInt32(input["ProductTypeId"]);
                objrequest.ShipmentValue = Convert.ToDecimal(input["ShipmentValue"]);
                objrequest.ToCounterId = Convert.ToString(input["ToCounter"]);
                objrequest.TranshipmentPoints = Convert.ToString(input["TranshipmentPoints"]);
                objrequest.LoginId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));

                DataTable dtweights = new DataTable();

                dtweights.Columns.Add("ParcelType", typeof(int));
                dtweights.Columns.Add("CalculationType", typeof(int));
                dtweights.Columns.Add("NumberOfPieces", typeof(int));
                dtweights.Columns.Add("ActualWeight", typeof(decimal));
                dtweights.Columns.Add("TotalWeight", typeof(decimal));

                if (input["ParcelItems"] != null)
                {
                    JArray parcelitems = (JArray)input["ParcelItems"];

                    foreach (JObject item in parcelitems)
                    {
                        DataRow dr = dtweights.NewRow();
                        dr["ParcelType"] = Convert.ToInt32(item["ParcelType"]);
                        dr["CalculationType"] = Convert.ToInt32(item["CalculationType"]);
                        dr["NumberOfPieces"] = Convert.ToInt32(item["NumberOfPieces"]);
                        dr["ActualWeight"] = Convert.ToDecimal(item["ActualWeight"]);
                        dr["TotalWeight"] = Convert.ToDecimal(item["TotalWeight"]);
                        dtweights.Rows.Add(dr);
                    }
                }

                objresponse = objBookingDal.GetCalculatedPriceForBooking(objrequest, dtweights);
            }
            catch (Exception ex)
            {
                
            }
            return objresponse;
        }

        public BookingSaveResponse InsertBookingDetails(JObject input)
        {
            BookingSaveResponse objresponse = new BookingSaveResponse();

            try
            {
                BookingRequest objrequest = new BookingRequest();

                objrequest.UserLoginId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));
                objrequest.FromCounterId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                objrequest.FromCounter = Convert.ToString(input["FromCounter"]);
                objrequest.ToCounter = Convert.ToString(input["ToCounter"]);
                objrequest.GCTypeId = Convert.ToInt32(input["GCTypeId"]);
                objrequest.BookingTypeId = Convert.ToInt32(input["BookingTypeId"]);
                objrequest.ProductTypeId = Convert.ToInt32(input["ProductTypeId"]);
                objrequest.PaidTypeId = Convert.ToInt32(input["PaidTypeId"]);
                objrequest.BookId = Convert.ToInt32(input["BookId"]);
                objrequest.SenderId = Convert.ToInt32(input["SenderId"]);
                objrequest.SenderName = Convert.ToString(input["SenderName"]);
                objrequest.SenderEmailId = Convert.ToString(input["SenderEmailId"]);
                objrequest.SenderMobileNumber = Convert.ToString(input["SenderMobileNumber"]);
                objrequest.SenderAddress = Convert.ToString(input["SenderAddress"]);
                objrequest.ReceiverId = Convert.ToInt32(input["ReceiverId"]);
                objrequest.ReceiverName = Convert.ToString(input["ReceiverName"]);
                objrequest.ReceiverEmailId = Convert.ToString(input["ReceiverEmailId"]);
                objrequest.ReceiverMobileNumber = Convert.ToString(input["ReceiverMobileNumber"]);
                objrequest.ReceiverAddress = Convert.ToString(input["ReceiverAddress"]);
                objrequest.TranshipmentPoint1 = Convert.ToString(input["TranshipmentPoint1"]);
                objrequest.TranshipmentPoint2 = Convert.ToString(input["TranshipmentPoint2"]);
                objrequest.ShipmentValue = Convert.ToDecimal(input["ShipmentValue"]);
                objrequest.ShipmentDescription = Convert.ToString(input["ShipmentDescription"]);
                objrequest.BasicAmount = Convert.ToDecimal(input["BasicAmount"]);
                objrequest.SUPCharges = Convert.ToDecimal(input["SUPCharges"]);
                objrequest.WithPASSCharges = Convert.ToDecimal(input["WithPASSCharges"]);
                objrequest.DocketCharges = Convert.ToDecimal(input["DocketCharges"]);
                objrequest.ValueSCCharges = Convert.ToDecimal(input["ValueSCCharges"]);
                objrequest.CollectionCharges = Convert.ToDecimal(input["CollectionCharges"]);
                objrequest.HamaliCharges = Convert.ToDecimal(input["HamaliCharges"]);
                objrequest.AOCCharges = Convert.ToDecimal(input["AOCCharges"]);
                objrequest.TranshipmentCharges = Convert.ToDecimal(input["TranshipmentCharges"]);
                objrequest.PickupCharges = Convert.ToDecimal(input["PickupCharges"]);
                objrequest.LocationPickupCharges = Convert.ToDecimal(input["LocationPickupCharges"]);
                objrequest.LocationDeliveryCharges = Convert.ToDecimal(input["LocationDeliveryCharges"]);
                objrequest.DoorDeliveryCharges = Convert.ToDecimal(input["DoorDeliveryCharges"]);
                objrequest.DriverCharges = Convert.ToDecimal(input["DriverCharges"]);
                objrequest.ToPayCharges = Convert.ToDecimal(input["ToPayCharges"]);
                objrequest.LRCharges = Convert.ToDecimal(input["LRCharges"]);
                objrequest.BookingOffCharges = Convert.ToDecimal(input["BookingOffCharges"]);
                objrequest.DeliveryCharges = Convert.ToDecimal(input["DeliveryCharges"]);
                objrequest.SubTotal = Convert.ToDecimal(input["SubTotal"]);
                objrequest.GSTCharges = Convert.ToDecimal(input["GSTCharges"]);
                objrequest.TotalAmount = Convert.ToDecimal(input["TotalAmount"]);
                objrequest.DiscountAmount = Convert.ToDecimal(input["DiscountAmount"]);
                objrequest.TotalAmountAfterDiscount = Convert.ToDecimal(input["TotalAmountAfterDiscount"]);
                objrequest.RoundOffAmount = Convert.ToDecimal(input["RoundOffAmount"]);
                objrequest.GrandTotal = Convert.ToDecimal(input["GrandTotal"]);
                objrequest.TotalKms = Convert.ToDecimal(input["TotalKms"]);
                objrequest.DiscountRemarks = Convert.ToString(input["DiscountRemarks"]);
                objrequest.EditPriceRemarks = Convert.ToString(input["EditPriceRemarks"]);
                objrequest.TotalPieces = Convert.ToInt32(input["TotalPieces"]);
                objrequest.TotalWeight = Convert.ToDecimal(input["TotalWeight"]);
                objrequest.WeightInfo = Convert.ToString(input["WeightInfo"]);
                objrequest.RouteInfo = Convert.ToString(input["RouteInfo"]);
                
                DataTable dtweights = new DataTable();

                dtweights.Columns.Add("ParcelType", typeof(int));
                dtweights.Columns.Add("CalculationType", typeof(int));
                dtweights.Columns.Add("NumberOfPieces", typeof(int));
                dtweights.Columns.Add("ActualWeight", typeof(decimal));
                dtweights.Columns.Add("TotalWeight", typeof(decimal));

                if (input["ParcelItems"] != null)
                {
                    JArray parcelitems = (JArray)input["ParcelItems"];

                    foreach (JObject item in parcelitems)
                    {
                        DataRow dr = dtweights.NewRow();
                        dr["ParcelType"] = Convert.ToInt32(item["ParcelType"]);
                        dr["CalculationType"] = Convert.ToInt32(item["CalculationType"]);
                        dr["NumberOfPieces"] = Convert.ToInt32(item["NumberOfPieces"]);
                        dr["ActualWeight"] = Convert.ToDecimal(item["ActualWeight"]);
                        dr["TotalWeight"] = Convert.ToDecimal(item["TotalWeight"]);
                        dtweights.Rows.Add(dr);
                    }
                }

                objresponse = objBookingDal.InsertBookingDetails(objrequest, dtweights);

                if(objresponse.StatusId == 1)
                {
                    CommonMethods.SendSMS(objrequest.SenderMobileNumber, objresponse.SenderMessage);
                    CommonMethods.SendSMS(objrequest.ReceiverMobileNumber, objresponse.ReceiverMessage);
                    string encryptedbookingnumber = CommonMethods.URLKeyDecrypt(objresponse.BookingSerialNumber);
                    int bookingid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(objresponse.BookingId));
                    string barcodeimage = CommonMethods.GenerateBarCode(encryptedbookingnumber);

                    if (!string.IsNullOrEmpty(barcodeimage))
                    {
                        objBookingDal.UpdateBookingBarcode(barcodeimage, bookingid);
                    }
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public CustomerDetailsResponse GetCustomerDetailsByMobileNumber(JObject input)
        {
            CustomerDetailsResponse objresponse = new CustomerDetailsResponse();

            try
            {
                string mobilenumber = Convert.ToString(input["MobileNumber"]);

                objresponse = objBookingDal.GetCustomerDetailsByMobileNumber(mobilenumber);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public BookingDetailsByBookingNumber GetBookingDetailsByBookingNumber(JObject input)
        {
            BookingDetailsByBookingNumber objresponse = new BookingDetailsByBookingNumber();

            try
            {
                string bookingnumber = Convert.ToString(input["BookingNumber"]);
                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));

                objresponse = objBookingDal.GetBookingDetailsByBookingNumber(bookingnumber, counterid);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public LoadingSaveResponse InsertLoadingDetails(JObject input)
        {
            LoadingSaveResponse objresponse = new LoadingSaveResponse();

            try
            {
                LoadingRequest objrequest = new LoadingRequest();

                objrequest.BookingIds = Convert.ToString(input["BookingIds"]);
                objrequest.CounterId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                objrequest.DriverAmount = Convert.ToDecimal(input["DriverAmount"]);
                objrequest.DriverMobileNumber = Convert.ToString(input["DriverMobileNumber"]);
                objrequest.DriverName = Convert.ToString(input["DriverName"]);
                objrequest.LoadingDateTime = Convert.ToString(input["LoadingDateTime"]);
                objrequest.EstimatedDateTime = Convert.ToString(input["EstimatedDateTime"]);
                objrequest.HamaliAmount = Convert.ToDecimal(input["HamaliAmount"]);
                objrequest.Remarks = Convert.ToString(input["Remarks"]);
                objrequest.UserLoginId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["UserLoginId"])));
                objrequest.VehicleNumber = Convert.ToString(input["VehicleNumber"]);
                objrequest.Route = Convert.ToString(input["Route"]);
                
                objresponse = objBookingDal.InsertLoadingDetails(objrequest);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public TobeReceiveMastersResponse GetMastersForToBeReceive(JObject input)
        {
            TobeReceiveMastersResponse objresponse = new TobeReceiveMastersResponse();

            try
            {
                DataSet ds = new DataSet();

                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                int loginid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));

                ds = objBookingDal.GetMastersForToBeReceive(counterid, loginid);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        var gctypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 1).
                                           Select(x => new GCTypesResponse
                                           {
                                               GCTypeId = x.Field<int>("Id"),
                                               GCType = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.GCTypes = gctypes;

                        var counters = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 2).
                                           Select(x => new CounterMastersResponse
                                           {
                                               CounterId = x.Field<int>("Id"),
                                               CounterName = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.Counters = counters;

                        var receivingfrom = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 3).
                                           Select(x => new ToBeReceivingFromResponse
                                           {
                                               InformationFromId = x.Field<int>("Id"),
                                               InformationFrom = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.ToBeReceivingFrom = receivingfrom;

                        objresponse.StatusId = 1;
                        objresponse.StatusMessage = "Valid";
                    }
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public SaveRespone InsertToBeReceiveDetails(JObject input)
        {
            SaveRespone objresponse = new SaveRespone();

            try
            {
                TobereceiveRequest objrequest = new TobereceiveRequest();
                
                objrequest.CounterId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                objrequest.UserLoginId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["UserLoginId"])));
                objrequest.DriverMobileNumber = Convert.ToString(input["DriverMobileNumber"]);
                objrequest.DriverName = Convert.ToString(input["DriverName"]);
                objrequest.EstimatedDateTime = Convert.ToString(input["EstimatedDateTime"]);
                objrequest.FromCounter = Convert.ToString(input["FromCounter"]);
                objrequest.GCBookingNumber = Convert.ToString(input["GCBookingNumber"]);
                objrequest.GCType = Convert.ToInt32(input["GCType"]);
                objrequest.Remarks = Convert.ToString(input["Remarks"]);
                objrequest.NumberOfPieces = Convert.ToInt32(input["NumberOfPieces"]);
                objrequest.InformationFromId = Convert.ToInt32(input["InformationFromId"]);

                objresponse = objBookingDal.InsertToBeReceiveDetails(objrequest);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public ReceivingMastersResponse GetMastersForReceiving(JObject input)
        {
            ReceivingMastersResponse objresponse = new ReceivingMastersResponse();

            try
            {
                DataSet ds = new DataSet();

                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                int loginid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));

                ds = objBookingDal.GetMastersForReceiving(counterid, loginid);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        var gctypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 1).
                                           Select(x => new GCTypesResponse
                                           {
                                               GCTypeId = x.Field<int>("Id"),
                                               GCType = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.GCTypes = gctypes;

                        var counters = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 2).
                                           Select(x => new CounterMastersResponse
                                           {
                                               CounterId = x.Field<int>("Id"),
                                               CounterName = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.Counters = counters;

                        var receivingtypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 3).
                                           Select(x => new ReceivingTypesResponse
                                           {
                                               ReceivingTypeId = x.Field<int>("Id"),
                                               ReceivingTypeName = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.ReceivingTypes = receivingtypes;

                        if (ds.Tables[1] != null)
                        {
                            var producttypes = ds.Tables[1].AsEnumerable().
                                               Select(x => new ProductTypesResponse
                                               {
                                                   ProductTypeId = x.Field<int>("Id"),
                                                   ProductType = x.Field<string>("Name"),
                                                   GCTypeId = x.Field<Nullable<int>>("GCTypeId")
                                               }).ToList();
                            objresponse.ProductTypes = producttypes;
                        }

                        objresponse.StatusId = 1;
                        objresponse.StatusMessage = "Valid";
                    }
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public ToBereceiveDetailsByBookingNumber GetToBeReceivedDetailsByBookingNumber(JObject input)
        {
            ToBereceiveDetailsByBookingNumber objresponse = new ToBereceiveDetailsByBookingNumber();

            try
            {
                string bookingnumber = Convert.ToString(input["BookingNumber"]);
                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));

                objresponse = objBookingDal.GetToBeReceivedDetailsByBookingNumber(bookingnumber, counterid);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public SaveRespone InsertReceivingDetails(JObject input)
        {
            SaveRespone objresponse = new SaveRespone();

            try
            {
                ReceivingRequest objrequest = new ReceivingRequest();

                objrequest.CounterId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                objrequest.UserLoginId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["UserLoginId"])));
                objrequest.DriverMobileNumber = Convert.ToString(input["DriverMobileNumber"]);
                objrequest.DriverName = Convert.ToString(input["DriverName"]);
                objrequest.BookingId = Convert.ToInt32(input["BookingId"]);
                objrequest.FromCounter = Convert.ToString(input["FromCounter"]);
                objrequest.GCBookingNumber = Convert.ToString(input["GCBookingNumber"]);
                objrequest.GCType = Convert.ToInt32(input["GCType"]);
                objrequest.ProductType = Convert.ToInt32(input["ProductType"]);
                objrequest.Remarks = Convert.ToString(input["Remarks"]);
                objrequest.NumberOfPieces = Convert.ToInt32(input["NumberOfPieces"]);
                objrequest.DeliveryToName = Convert.ToString(input["DeliveryToName"]);
                objrequest.DeliveryToNumber = Convert.ToString(input["DeliveryToNumber"]);
                objrequest.HamaliCharges = Convert.ToDecimal(input["HamaliCharges"]);
                objrequest.ReceivingType = Convert.ToInt32(input["ReceivingType"]);
                objrequest.TranshipmentCharges = Convert.ToInt32(input["TranshipmentCharges"]);
                objrequest.TotalWeight = Convert.ToInt32(input["TotalWeight"]);
                objrequest.VehicleNumber = Convert.ToString(input["VehicleNumber"]);
                objrequest.ToBeReceiveId = Convert.ToInt32(input["ToBeReceiveId"]);
                objrequest.BillAmount = Convert.ToDecimal(input["BillAmount"]);

                objresponse = objBookingDal.InsertReceivingDetails(objrequest);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public BookingDetailsForPrintResponse GetBookingDetailsToPrintByBookingId(JObject input)
        {
            BookingDetailsForPrintResponse objresponse = new BookingDetailsForPrintResponse();

            try
            {
                int bookingid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["BookingId"])));

                objresponse = objBookingDal.GetBookingDetailsToPrintByBookingId(bookingid);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public LoadingDetailsForPrintResponse GetLoadingDetailsToPrintByLoadingId(JObject input)
        {
            LoadingDetailsForPrintResponse objresponse = new LoadingDetailsForPrintResponse();

            try
            {
                int loadingid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoadingId"])));

                objresponse = objBookingDal.GetLoadingDetailsToPrintByLoadingId(loadingid);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public DeliveryMastersResponse GetMastersForDelivery(JObject input)
        {
            DeliveryMastersResponse objresponse = new DeliveryMastersResponse();

            try
            {
                DataSet ds = new DataSet();

                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                int loginid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));

                ds = objBookingDal.GetMastersForDelivery(counterid, loginid);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        var gctypes = ds.Tables[0].AsEnumerable().Where(a => a.Field<int>("MasterType") == 1).
                                           Select(x => new GCTypesResponse
                                           {
                                               GCTypeId = x.Field<int>("Id"),
                                               GCType = x.Field<string>("Name")
                                           }).ToList();

                        objresponse.GCTypes = gctypes;

                        var productTypes = ds.Tables[1].AsEnumerable().
                                          Select(x => new ProductTypesResponse
                                          {
                                              ProductTypeId = x.Field<int>("ProductTypeId"),
                                              ProductType = x.Field<string>("ProductType"),
                                              GCTypeId = x.Field<Nullable<int>>("GC_Type_Id")
                                          }).ToList();

                        objresponse.ProductTypes = productTypes;

                        objresponse.StatusId = 1;
                        objresponse.StatusMessage = "Valid";
                    }
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public ReceiveDetailsByBookingNumber GetReceivingDetailsByBookingNumber(JObject input)
        {
            ReceiveDetailsByBookingNumber objresponse = new ReceiveDetailsByBookingNumber();

            try
            {
                string bookingnumber = Convert.ToString(input["BookingNumber"]);
                int counterid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));

                objresponse = objBookingDal.GetReceivingDetailsByBookingNumber(bookingnumber, counterid);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        public DeliverySaveResponse InsertDeliveryDetails(JObject input)
        {
            DeliverySaveResponse objresponse = new DeliverySaveResponse();

            try
            {
                DeliveryRequest objrequest = new DeliveryRequest();

                objrequest.CounterId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                objrequest.UserLoginId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["UserLoginId"])));
                objrequest.BookingId = Convert.ToInt32(input["BookingId"]);
                objrequest.GCBookingNumber = Convert.ToString(input["GCBookingNumber"]);
                objrequest.GCType = Convert.ToInt32(input["GCType"]);
                objrequest.ProductType = Convert.ToInt32(input["ProductType"]);
                objrequest.Remarks = Convert.ToString(input["Remarks"]);
                objrequest.DeliveryCharges = Convert.ToDecimal(input["DeliveryCharges"]);
                objrequest.DemoCharges = Convert.ToDecimal(input["DemoCharges"]);
                objrequest.ReceivingId = Convert.ToInt32(input["ReceivingId"]);
                objrequest.DeliveryPhoneNumber = Convert.ToString(input["DeliveryPhoneNumber"]);
                objrequest.DeliveryTo = Convert.ToString(input["DeliveryTo"]);
                objrequest.BillAmount = Convert.ToDecimal(input["BillAmount"]);

                objresponse = objBookingDal.InsertDeliveryDetails(objrequest);

                if (objresponse.StatusId == 1)
                {
                    CommonMethods.SendSMS(objrequest.DeliveryPhoneNumber, "Your LR Number: "+ objrequest.GCBookingNumber + " is delivered successfully to "+ objrequest.DeliveryTo + ", Mobile No: "+ objrequest.DeliveryPhoneNumber + "");
                }
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }
        public DeliveryDetailsForPrintResponse GetDeliveryDetailsToPrintByDeliveryId(JObject input)
        {
            DeliveryDetailsForPrintResponse objresponse = null;

            try
            {
                int deliveryId = Convert.ToInt32(Convert.ToString(input["DeliveryId"]));

                objresponse = objBookingDal.GetDeliveryDetailsToPrintByDeliveryId(deliveryId);
            }
            catch (Exception ex)
            {
               
            }
            return objresponse;
        }

        #endregion
    }
}
