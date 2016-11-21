using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.DocReturned {
    public class FollowDocReturnViewModels
    {
        public string Department { get; set; }
        public string Section { get; set; }
        public int Delay10 { get; set; }
        public int Delay30 { get; set; }
        public int Delay60 { get; set; }
        public int Delay90 { get; set; }
        public int Total { get; set; }
        public int More10 { get; set; }
    }
}