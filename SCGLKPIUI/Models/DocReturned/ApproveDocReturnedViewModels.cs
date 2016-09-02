using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models {
    public class ApproveDocReturnedViewModels {
        public string Dn { get; set; }
        public string CarrierId { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string Soldto { get; set; }
        public string SoldtoName { get; set; }
        public string Shipto { get; set; }
        public string ShiptoName { get; set; }
        public DateTime? PlanDocReturn { get; set; } //PLNDOCRETDATE_SCGL
        public DateTime? ActualDocReturn { get; set; } //DOCRETDATE_SCGL
        public DateTime? ActualGI { get; set; } //ACTGIDATE
        public string thisReasonId { get; set; }
        public bool Approve { get; set; }
        public string Reason { get; set; } //addded
        public int Adjust { get; set; } //added
        public string AdjustBy { get; set; } //added
        public string Remark { get; set; }
    }
}