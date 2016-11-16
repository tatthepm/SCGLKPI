using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Inbounded {
    public class PendingInboundViewModels {
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
        public string LTenderDate { get; set; } //LastTender
        public string PlanInbound { get; set; } //PLNINBDATE
        public string Delays { get; set; }
    }
}