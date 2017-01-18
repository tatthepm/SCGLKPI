using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models
{
    public class ReportSalesViewModels
    {
        public string ActualGiDate { get; set; }
        public string Segment { get; set; }
        public string Customer { get; set; }
        public string MatName { get; set; }
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
    }
}