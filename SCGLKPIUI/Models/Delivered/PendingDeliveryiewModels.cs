using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Delivered {
    public class PendingDeliveryViewModels {
        public string Shipment { get; set; }
        public string DeliveryNote { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public string ShippingPoint { get; set; }
        public string TruckType { get; set; }
        public string OrderCompDate { get; set; }
        public string ShcrDate { get; set; }
        public string RequestedDate { get; set; }
        public string PlanDelivery { get; set; } //PLNONTIMEDATE
    }
}