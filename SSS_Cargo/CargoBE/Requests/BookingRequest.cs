using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBE.Requests
{
    public class BookingRequest
    {
        public int UserLoginId { get; set; }
        public int FromCounterId { get; set; }
        public string FromCounter { get; set; }
        public string ToCounter { get; set; }
        public int GCTypeId { get; set; }
        public int BookingTypeId { get; set; }
        public int ProductTypeId { get; set; }
        public int BookId { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmailId { get; set; }
        public string SenderMobileNumber { get; set; }
        public string SenderAddress { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmailId { get; set; }
        public string ReceiverMobileNumber { get; set; }
        public string ReceiverAddress { get; set; }
        public string TranshipmentPoint1 { get; set; }
        public string TranshipmentPoint2 { get; set; }
        public decimal ShipmentValue { get; set; }
        public string ShipmentDescription { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal SUPCharges { get; set; }
        public decimal WithPASSCharges { get; set; }
        public decimal DocketCharges { get; set; }
        public decimal ValueSCCharges { get; set; }
        public decimal CollectionCharges { get; set; }
        public decimal HamaliCharges { get; set; }
        public decimal AOCCharges { get; set; }
        public decimal TranshipmentCharges { get; set; }
        public decimal PickupCharges { get; set; }
        public decimal LocationPickupCharges { get; set; }
        public decimal LocationDeliveryCharges { get; set; }
        public decimal DoorDeliveryCharges { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GSTCharges { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmountAfterDiscount { get; set; }
        public decimal RoundOffAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalKms { get; set; }
        public string DiscountRemarks { get; set; }
        public string EditPriceRemarks { get; set; }
        public int TotalPieces { get; set; }
        public decimal TotalWeight { get; set; }
        public string WeightInfo { get; set; }
        public string RouteInfo { get; set; }
    }
    
    public class BookingPriceCalcRequest
    {
        public int LoginId { get; set; }
        public int FromCounterId { get; set; }
        public string ToCounterId { get; set; }
        public int GCType { get; set; }
        public int ProductType { get; set; }
        public string TranshipmentPoints { get; set; }
        public decimal ShipmentValue { get; set; }
    }

    public class LoadingRequest
    {
        public int UserLoginId { get; set; }
        public int CounterId { get; set; }
        public string DriverName { get; set; }
        public string DriverMobileNumber { get; set; }
        public string VehicleNumber { get; set; }
        public decimal DriverAmount { get; set; }
        public decimal HamaliAmount { get; set; }
        public string LoadingDateTime { get; set; }
        public string EstimatedDateTime { get; set; }
        public string Remarks { get; set; }
        public string BookingIds { get; set; }
    }
    public class TobereceiveRequest
    {
        public int UserLoginId { get; set; }
        public int CounterId { get; set; }
        public string GCBookingNumber { get; set; }
        public int GCType { get; set; }
        public string FromCounter { get; set; }
        public int NumberOfPieces { get; set; }
        public string DriverName { get; set; }
        public string DriverMobileNumber { get; set; }
        public string EstimatedDateTime { get; set; }
        public string Remarks { get; set; }
    }

    public class ReceivingRequest
    {
        public int UserLoginId { get; set; }
        public int ToBeReceiveId { get; set; }
        public int CounterId { get; set; }
        public int BookingId { get; set; }
        public string GCBookingNumber { get; set; }
        public int GCType { get; set; }
        public string FromCounter { get; set; }
        public int ReceivingType { get; set; }
        public int NumberOfPieces { get; set; }
        public string VehicleNumber { get; set; }
        public string DriverName { get; set; }
        public string DriverMobileNumber { get; set; }
        public decimal HamaliCharges { get; set; }
        public decimal TranshipmentCharges { get; set; }
        public string DeliveryToName { get; set; }
        public string DeliveryToNumber { get; set; }
        public string Remarks { get; set; }
    }
}
