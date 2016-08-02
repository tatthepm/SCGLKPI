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
        [Column(Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength (10)]
        public string month_year { get; set; }
        [StringLength(5)]
        [Column(Order = 1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [StringLength(5)]
        [Column(Order = 2), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        [StringLength(20)]
        [Column(Order = 3), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Segment { get; set; }
        public int daysDIff { get; set; }
        public int LastMonth { get; set; }
        public int Year { get; set; }
        public int shipmentLastMonthCount { get; set; }
        public int DNLastMonthCount { get; set; }
        public int OntimeCount { get; set; }
        public int DelayCount { get; set; }
        public int PendingCount { get; set; }
        public int TenderLastMonthCount { get; set; }
        public int AcceptLastMonthCount { get; set; }
        public int InboundLastMonthCount { get; set; }
        public int OutboundLastMonthCount { get; set; }
        public int DeliveryLastMonthCount { get; set; }
        public int DocReturnLastMonthCount { get; set; }
        public int TenderOntimeCount { get; set; }
        public int AcceptOntimeCount { get; set; }
        public int InboundOntimeCount { get; set; }
        public int OutboundOntimeCount { get; set; }
        public int DeliveryOntimeCount { get; set; }
        public int DocReturnOntimeCount { get; set; }
        public int TenderDelayCount { get; set; }
        public int AcceptDelayCount { get; set; }
        public int InboundDelayCount { get; set; }
        public int OutboundDelayCount { get; set; }
        public int DeliveryDelayCount { get; set; }
        public int DocReturnDelayCount { get; set; }
        public int TenderPendingCount { get; set; }
        public int AcceptPendingCount { get; set; }
        public int InboundPendingCount { get; set; }
        public int OutboundPendingCount { get; set; }
        public int DeliveryPendingCount { get; set; }
        public int DocReturnPendingCount { get; set; }
    }
}
