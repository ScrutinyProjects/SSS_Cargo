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
        public List<BookingParcelDetails> BookingParcels { get; set; }
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
}
