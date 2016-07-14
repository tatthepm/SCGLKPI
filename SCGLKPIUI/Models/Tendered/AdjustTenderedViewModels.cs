using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Tendered {
    public class AdjustTenderedViewModels {
        public string Shipment { get; set; }
        public string CarrierId { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public DateTime? PlanTender { get; set; }
        public DateTime? FirstTender { get; set; }
        public string ReasonId { get; set; }
    }
}