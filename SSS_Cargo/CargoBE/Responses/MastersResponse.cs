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

    public class ParcelTypesResponse
    {
        public int ParcelTypeId { get; set; }
        public string ParcelType { get; set; }
    }

    public class ProductTypesResponse
    {
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
    }

    public class BooksMasterResponse
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
}
