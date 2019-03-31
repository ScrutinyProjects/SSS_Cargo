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

        public BookingSaveResponse InsertBookingDetails(JObject input)
        {
            BookingSaveResponse objresponse = new BookingSaveResponse();

            try
            {
                BookingRequest objrequest = new BookingRequest();

                objrequest.AOCCharges = Convert.ToDecimal(input["AOCCharges"]);
                objrequest.BasicAmount = Convert.ToDecimal(input["BasicAmount"]);
                objrequest.BookId = Convert.ToInt32(input["BookId"]);
                objrequest.BookingTypeId = Convert.ToInt32(input["BookingTypeId"]);
                objrequest.CollectionCharges = Convert.ToDecimal(input["CollectionCharges"]);
                objrequest.DocketCharges = Convert.ToDecimal(input["DocketCharges"]);
                objrequest.FromCounterId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"])));
                objrequest.GCTypeId = Convert.ToInt32(input["GCTypeId"]);
                objrequest.GSTCharges = Convert.ToDecimal(input["GSTCharges"]);
                objrequest.HamaliCharges = Convert.ToDecimal(input["HamaliCharges"]);
                objrequest.ProductTypeId = Convert.ToInt32(input["ProductTypeId"]);
                objrequest.ReceiverAddress = Convert.ToString(input["ReceiverAddress"]);
                objrequest.ReceiverEmailId = Convert.ToString(input["ReceiverEmailId"]);
                objrequest.ReceiverId = Convert.ToInt32(input["ReceiverId"]);
                objrequest.ReceiverMobileNumber = Convert.ToString(input["BasicAmount"]);
                objrequest.ReceiverName = Convert.ToString(input["BasicAmount"]);
                objrequest.SenderAddress = Convert.ToString(input["SenderAddress"]);
                objrequest.SenderEmailId = Convert.ToString(input["SenderEmailId"]);
                objrequest.SenderId = Convert.ToInt32(input["SenderId"]);
                objrequest.SenderMobileNumber = Convert.ToString(input["SenderMobileNumber"]);
                objrequest.SenderName = Convert.ToString(input["SenderName"]);
                objrequest.ShipmentDescription = Convert.ToString(input["ShipmentDescription"]);
                objrequest.ShipmentValue = Convert.ToDecimal(input["ShipmentValue"]);
                objrequest.SUPCharges = Convert.ToDecimal(input["SUPCharges"]);
                objrequest.ToCounterId = Convert.ToInt32(input["ToCounterId"]);
                objrequest.TotalAmount = Convert.ToDecimal(input["TotalAmount"]);
                objrequest.TranshipmentCharges = Convert.ToDecimal(input["TranshipmentCharges"]);
                objrequest.TranshipmentPoints = Convert.ToString(input["TranshipmentPoints"]);
                objrequest.UserLoginId = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));
                objrequest.ValueSCCharges = Convert.ToDecimal(input["ValueSCCharges"]);
                objrequest.WithPASSCharges = Convert.ToDecimal(input["WithPASSCharges"]);

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
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        #endregion
    }
}
