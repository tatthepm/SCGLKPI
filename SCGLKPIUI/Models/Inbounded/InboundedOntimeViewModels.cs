using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Inbounded {
    public class InboundedOntimeViewModels {
        public string ActualGiDate { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string MatName { get; set; }
        public int SumOfInbound { get; set; }
        public int OnTime { get; set; }
        public int Delay { get; set; }
        public int AdjustInbound { get; set; }
        public double Percent { get; set; }
        public double PercentAdjust { get; set; }
    }
}