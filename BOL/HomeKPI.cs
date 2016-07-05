using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    public class HomeKPI
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string month_year { get; set; }
        [StringLength(10)]
        public string daysDIff { get; set; }
        [StringLength(10)]
        public string LastMonth { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        [StringLength(15)]
        public string shipmentLastMonthCount { get; set; }
        [StringLength(15)]
        public string DNLastMonthCount { get; set; }
        [StringLength(15)]
        public string PercentOntime { get; set; }
        [StringLength(15)]
        public string PercentDelay { get; set; }
        [StringLength(15)]
        public string PercentPending { get; set; }
        [StringLength(15)]
        public string OntimeCount { get; set; }
        [StringLength(15)]
        public string DelayCount { get; set; }
        [StringLength(15)]
        public string PendingCount { get; set; }
        [StringLength(15)]
        public string TenderLastMonthCount { get; set; }
        [StringLength(15)]
        public string AcceptLastMonthCount { get; set; }
        [StringLength(15)]
        public string InboundLastMonthCount { get; set; }
        [StringLength(15)]
        public string OutboundLastMonthCount { get; set; }
        [StringLength(15)]
        public string DeliveryLastMonthCount { get; set; }
        [StringLength(15)]
        public string DocReturnLastMonthCount { get; set; }
    }
}
