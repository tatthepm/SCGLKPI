using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Tendered {
    public class TenderedOntimeSummaryMonthlyViewModels {
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public int SumOfTender { get; set; }
        public int OnTime { get; set; }
        public int Delay { get; set; }
        public int Adjust { get; set; }
        public double Plan { get; set; }
        public double Percent { get; set; }
        public double PercentAdjust { get; set; }
    }
}