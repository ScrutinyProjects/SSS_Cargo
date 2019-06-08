using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBE.Responses
{
    public class MastersResponse
    {

    }

    public class GCTypesResponse
    {
        public int GCTypeId { get; set; }
        public string GCType { get; set; }
    }

    public class CounterMastersResponse
    {
        public int CounterId { get; set; }
        public string CounterName { get; set; }
    }

    public class ToBeReceivingFromResponse
    {
        public int InformationFromId { get; set; }
        public string InformationFrom { get; set; }
    }

    public class ParcelTypesResponse
    {
        public int ParcelTypeId { get; set; }
        public string ParcelType { get; set; }
    }

    public class ProductTypesResponse
    {
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }

        public int? GCTypeId { get; set; }
    }

    public class BooksMasterResponse
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
    public class ReceivingTypesResponse
    {
        public int ReceivingTypeId { get; set; }
        public string ReceivingTypeName { get; set; }
    }

    public class CustomersMasterResponse
    {
        public int CustomerId { get; set; }
        public string MobileNumber { get; set; }
    }

    public class PaidTypesMasterResponse
    {
        public int PaidTypeId { get; set; }
        public string PaidTypeName { get; set; }
    }

    public class ExpenseTypeResponse
    {
        public int ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
    }

    public class BookingStatusResponse
    {
        public int BookingStatusId { get; set; }
        public string BookingStatus { get; set; }
    }
}
