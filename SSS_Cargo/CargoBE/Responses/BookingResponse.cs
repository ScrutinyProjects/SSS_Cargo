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
    }
}
