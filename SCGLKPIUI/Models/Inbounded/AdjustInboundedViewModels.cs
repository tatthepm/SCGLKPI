﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Inbounded {
    public class AdjustInboundedViewModels {
        public string DeliveryNote { get; set; }
        public string CarrierId { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public DateTime? PlanInbound { get; set; } //PLNINBDATE
        public DateTime? ActualInbound { get; set; } //ACTGIDATE
        public DateTime? ActualGI { get; set; } //ACTGIDATE
        public string ReasonId { get; set; }
    }
}