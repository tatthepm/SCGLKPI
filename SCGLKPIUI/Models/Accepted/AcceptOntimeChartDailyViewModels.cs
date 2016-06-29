using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models {
    public class AcceptOntimeChartDailyViewModels {
        public string AcceptedDate { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public double Plan { get; set; }
        public double Actual { get; set; }
        public double Adjust { get; set; }
        public int SumOfAccept { get; set; }
        public int PlanOfAccept { get; set; }
    }
}