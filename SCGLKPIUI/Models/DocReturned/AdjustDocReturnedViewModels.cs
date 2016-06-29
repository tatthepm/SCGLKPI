using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.DocReturned {
    public class AdjustDocReturnedViewModels {
        public string DeliveryNote { get; set; }
        public string CarrierId { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public DateTime? PlanDocReturn { get; set; } //PLNDOCRETDATE_SCGL
        public DateTime? ActualDocReturn { get; set; } //DOCRETDATE_SCGL
    }
}