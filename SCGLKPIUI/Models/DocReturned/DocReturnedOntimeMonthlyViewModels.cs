using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.DocReturned {
    public class DocReturnedOntimeMonthlyViewModels {
        public string Month { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string MatName { get; set; }
        public int SumOfDocReturn { get; set; }
        public int OnTime { get; set; }
        public int Delay { get; set; }
        public int AdjustDocReturn { get; set; }
        public double Plan { get; set; }
        public double Percent { get; set; }
        public double PercentAdjust { get; set; }
    }
}