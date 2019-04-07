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
        public string TranshipmentPoints { get; set; }
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
        public decimal GSTCharges { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalKms { get; set; }
    }
}
