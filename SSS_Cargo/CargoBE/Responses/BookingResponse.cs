using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBE.Responses
{
    public class BookingResponse
    {

    }

    public class BookingSaveResponse
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public string BookingSerialNumber { get; set; }
        public string BookingId { get; set; }
        public string SenderMessage { get; set; }
        public string ReceiverMessage { get; set; }
    }

    public class BookingCalculatedPriceResponse
    {
        public decimal BasicAmount { get; set; }
        public decimal SUPCharges { get; set; }
        public decimal WithPassCharges { get; set; }
        public decimal DocketCharges { get; set; }
        public decimal ValueSRCharges { get; set; }
        public decimal CollectionCharges { get; set; }
        public decimal HamaliCharges { get; set; }
        public decimal AOCCharges { get; set; }
        public decimal TranshipmentCharges { get; set; }
        public decimal PickupCharges { get; set; }
        public decimal LocationPickupCharges { get; set; }
        public decimal LocationDeliveryCharges { get; set; }
        public decimal DoorDeliveryCharges { get; set; }
        public decimal TotalKms { get; set; }
    }
    
    public class BookingMastersResponse
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public List<GCTypesResponse> GCTypes { get; set; }
        public List<CounterMastersResponse> Counters { get; set; }
        public List<ParcelTypesResponse> ParcelTypes { get; set; }
        public List<ProductTypesResponse> ProductTypes { get; set; }
        public List<BooksMasterResponse> Books { get; set; }
        public List<CustomersMasterResponse> Customers { get; set; }
    }

    public class TobeReceiveMastersResponse
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public List<GCTypesResponse> GCTypes { get; set; }
        public List<CounterMastersResponse> Counters { get; set; }
    }

    public class ReceivingMastersResponse
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public List<GCTypesResponse> GCTypes { get; set; }
        public List<CounterMastersResponse> Counters { get; set; }
        public List<ReceivingTypesResponse> ReceivingTypes { get; set; }
    }

    public class CustomerDetailsResponse
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }

    public class BookingDetailsByBookingNumber
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public int BookingId { get; set; }
        public int GCTypeId { get; set; }
        public string GCType { get; set; }
        public string MeasurementIn { get; set; }
        public string BookSerialNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string FromCounterName { get; set; }
        public string ToCounterName { get; set; }
        public string TranshipmentPoints { get; set; }
        public List<BookingParcelDetails> BookingParcels { get; set; }
    }

    public class ToBereceiveDetailsByBookingNumber
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public int BookingId { get; set; }
        public int ToBeReceiveId { get; set; }
        public int GCTypeId { get; set; }
        public string GCType { get; set; }
        public string MeasurementIn { get; set; }
        public string GCBookingNumber { get; set; }
        public string FromCounterName { get; set; }
        public int NumberOfPieces { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string EstimatedDateTime { get; set; }
        public string Remarks { get; set; }
        public decimal TotalWeight { get; set; }
    }
    
    public class BookingParcelDetails
    {
        public int BookingParcelId { get; set; }
        public int ParcelTypeId { get; set; }
        public string ParcelType { get; set; }
        public int NumberOfPieces { get; set; }
        public decimal ActualWeight { get; set; }
        public decimal TotalWeight { get; set; }
    }

    public class BookingDetailsForPrintResponse
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public string BookSerialNumber { get; set; }
        public string GCType { get; set; }
        public string BookingType { get; set; }
        public string PaymentType { get; set; }
        public string ProductType { get; set; }
        public string BookingDate { get; set; }
        public string SenderName { get; set; }
        public string SenderMobileNumber { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverMobileNumber { get; set; }
        public string FromCounterName { get; set; }
        public string FromCounterGST { get; set; }
        public string FromCounterPhoneNumber { get; set; }
        public string ToCounterName { get; set; }
        public string ToCounterGST { get; set; }
        public string ToCounterPhoneNumber { get; set; }
        public string ActualWeight { get; set; }
        public string ChargedWeight { get; set; }
        public int TotalPieces { get; set; }
        public decimal ShipmentValue { get; set; }
        public string ShipmentDescription { get; set; }
        public string RouteInfo { get; set; }
        public string CargoGSTIN { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal HamaliCharges { get; set; }
        public decimal SurCharges { get; set; }
        public decimal ValueSurCharges { get; set; }
        public decimal WithPassCharges { get; set; }
        public decimal AOCCharges { get; set; }
        public decimal TranshipmentCharges { get; set; }
        public decimal CollectionCharges { get; set; }
        public decimal PickupCharges { get; set; }
        public decimal LocationDeliveryCharges { get; set; }
        public decimal DoorDeliveryCharges { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal RoundOffAmount { get; set; }
        public decimal GrandTotal { get; set; }
    }
}