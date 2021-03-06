﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.DocReturned {
    public class DocReturnedOntimeChartDailyViewModels {
        public string ActualGiDate { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public double Plan { get; set; }
        public double Actual { get; set; }
        public double Adjust { get; set; }
        public int SumOfDocReturn { get; set; }
        public int PlanOfDocReturn { get; set; }
    }
}