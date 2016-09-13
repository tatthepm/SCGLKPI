using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models {
    public class ApproveDeliveredViewModels {
        public string Dn { get; set; }
        public string CarrierId { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public string ShippingPoint { get; set; }
        public string TruckType { get; set; }
        public string PlanDelivery { get; set; } //PLNONTIMEDATE
        public string ActualDelivery { get; set; } //ACDLVDATE
        public string ActualGI { get; set; } //ACTGIDATE
        public string thisReasonId { get; set; }
        public bool Approve { get; set; }
        public string Reason { get; set; } //addded
        public int Adjust { get; set; } //added
        public string AdjustBy { get; set; } //added
        public string Remark { get; set; }
    }
}