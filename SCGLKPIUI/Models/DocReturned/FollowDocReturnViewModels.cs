using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCGLKPIUI.Models.DocReturned {
    public class FollowDocReturnViewModels
    {
        public string Department { get; set; }
        public string Section { get; set; }
        public int DelayNormal { get; set; }
        public int DelayAlert { get; set; }
        public int DelayAlarm { get; set; }
        public int DelaySuperAlarm { get; set; }
        public int Total { get; set; }
        public int More10 { get; set; }
    }

    public class FollowDocReturnHubViewModels
    {
        public string Department { get; set; }
        public string HubName { get; set; }
        public int DelayNormal { get; set; }
        public int DelayAlert { get; set; }
        public int DelayAlarm { get; set; }
        public int DelaySuperAlarm { get; set; }
        public int Total { get; set; }
        public int More10 { get; set; }
    }
}