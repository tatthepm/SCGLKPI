using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCGLKPIUI.Models {
    public class AcceptOntimeViewModels {

        public string AcceptDate { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string MatName { get; set; }
        public int SumOfAccept { get; set; }
        public int OnTime { get; set; }
        public int Delay { get; set; }
        public int AdjustAccept { get; set; }
        public double Percent { get; set; }
        public double PercentAdjust { get; set; }

    }
}