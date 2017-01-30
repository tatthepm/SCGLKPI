using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class OntimeReports {
        public int Id { get; set; }
        //Master Data ===========================================================
        [Column(TypeName = "datetime2")]
        public DateTime? ActualGiDate { get; set; }
        [StringLength(5)]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [StringLength(5)]
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        [StringLength(20)]
        public string Segment { get; set; }
        [StringLength(20)]
        public string SubSegment { get; set; }
        public string MatName { get; set; }
        public string MatFriGrp { get; set; }
        [StringLength(20)]
        public string TruckType { get; set; }
        [StringLength(10)]
        public string SoldToId { get; set; }
        [StringLength(800)]
        public string SoldToName { get; set; }
        [StringLength(10)]
        public string CarrierId { get; set; }
        [StringLength(800)]
        public string CarrierName { get; set; }
        //KPI Data ===========================================================
        public int SumOfTender { get; set; }
        public double OnTimeTender { get; set; }
        public double AdjustTender { get; set; }
        public int SumOfAccept { get; set; }
        public double OnTimeAccept { get; set; }
        public double AdjustAccept { get; set; }
        public int SumOfInbound { get; set; }
        public double OnTimeInbound { get; set; }
        public double AdjustInbound { get; set; }
        public int SumOfOutbound { get; set; }
        public double OnTimeOutbound { get; set; }
        public double AdjustOutbound { get; set; }
        public int SumOfOntime { get; set; }
        public double OnTimeOntime { get; set; }
        public double AdjustOntime { get; set; }
        public int SumOfDocReturn { get; set; }
        public double OnTimeDocReturn { get; set; }
        public double AdjustDocReturn { get; set; }
        public double Plan { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }
    }
}
