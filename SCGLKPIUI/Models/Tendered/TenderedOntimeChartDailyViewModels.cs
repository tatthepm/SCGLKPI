using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models {
    public class TenderedOntimeChartDailyViewModels {
        public string ActualGiDate { get; set; }
        public string SegmentName { get; set; }
        public string SectionName { get; set; }
        public double Plan { get; set; }
        public double Actual { get; set; }
        public double Adjust { get; set; }
        public int SumOfTender { get; set; }
        public int PlanOfTender { get; set; }
    }
}