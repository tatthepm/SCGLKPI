using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Outbounded {
    public class AdjustOutboundedViewModels {
        public string Shipment { get; set; }
        public string DeliveryNote { get; set; }
        public string CarrierId { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public string ShippingPoint { get; set; }
        public string TruckType { get; set; }
        public string PlanOutbound { get; set; } //PLNOUTDATE
        public string ActualOutbound { get; set; } //ACDLVDATE
        public string ActualGI { get; set; } //ACTGIDATE
        public string ReasonId { get; set; }
    }
}