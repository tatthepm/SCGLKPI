using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models
{
    public class HomeModels
    {
        public string month_year { get; set; }
        public string daysDIff { get; set; }
        public string LastMonth { get; set; }
        public string Year { get; set; }
        public string shipmentLastMonthCount { get; set; }
        public string DNLastMonthCount { get; set; }
        public string PercentOntime { get; set; }
        public string PercentDelay { get; set; }
        public string PercentPending { get; set; }
        public string OntimeCount { get; set; }
        public string DelayCount { get; set; }
        public string PendingCount { get; set; }
        public string TenderLastMonthCount { get; set; }
        public string AcceptLastMonthCount { get; set; }
        public string InboundLastMonthCount { get; set; }
        public string OutboundLastMonthCount { get; set; }
        public string DeliveryLastMonthCount { get; set; }
        public string DocReturnLastMonthCount { get; set; }

    }
}