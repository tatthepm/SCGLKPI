using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Delivered {
    public class AdjustDeliveredViewModels {
        public string DeliveryNote { get; set; }
        public string CarrierId { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public DateTime? PlanDelivery { get; set; } //PLNONTIMEDATE
        public DateTime? ActualDelivery { get; set; } //ACDLVDATE

        public string ReasonId { get; set; }
    }
}