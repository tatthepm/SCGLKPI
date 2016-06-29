using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.Inbounded {
    public class InboundedOntimeChartYearlyViewModels {
        public string Year { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public double Plan { get; set; }
        public double Actual { get; set; }
        public double Adjust { get; set; }
        public int SumOfInbound { get; set; }
        public int PlanOfInbound { get; set; }
    }
}