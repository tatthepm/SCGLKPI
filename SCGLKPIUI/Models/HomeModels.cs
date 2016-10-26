using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BOL;
using BLL;

namespace SCGLKPIUI.Models
{
    public class HomeModels
    {
        public int DN01 { get; set; }
        public int DN02 { get; set; }
        public int DN03 { get; set; }
        public int DN04 { get; set; }
        public int DN05 { get; set; }
        public int DN06 { get; set; }
        public int DN07 { get; set; }
        public int DN08 { get; set; }
        public int DN09 { get; set; }
        public int DN10 { get; set; }
        public int DN11 { get; set; }
        public int DN12 { get; set; }

        public int SH01 { get; set; }
        public int SH02 { get; set; }
        public int SH03 { get; set; }
        public int SH04 { get; set; }
        public int SH05 { get; set; }
        public int SH06 { get; set; }
        public int SH07 { get; set; }
        public int SH08 { get; set; }
        public int SH09 { get; set; }
        public int SH10 { get; set; }
        public int SH11 { get; set; }
        public int SH12 { get; set; }

        public string month_year { get; set; }
        public string daysDIff { get; set; }
        public string LastMonth { get; set; }
        public string Year { get; set; }
        public string shipmentLastMonthCount { get; set; }
        public string DNLastMonthCount { get; set; }
        public string shipmentThisMonthCount { get; set; }
        public string DNThisMonthCount { get; set; }
        public string TenderLastMonthCount { get; set; }
        public string AcceptLastMonthCount { get; set; }
        public string InboundLastMonthCount { get; set; }
        public string OutboundLastMonthCount { get; set; }
        public string DeliveryLastMonthCount { get; set; }
        public string DocReturnLastMonthCount { get; set; }
        public string TenderLastMonthOntime { get; set; }
        public string AcceptLastMonthOntime { get; set; }
        public string InboundLastMonthOntime { get; set; }
        public string OutboundLastMonthOntime { get; set; }
        public string DeliveryLastMonthOntime { get; set; }
        public string DocReturnLastMonthOntime { get; set; }
        public string TenderLastMonthDelay { get; set; }
        public string AcceptLastMonthDelay { get; set; }
        public string InboundLastMonthDelay { get; set; }
        public string OutboundLastMonthDelay { get; set; }
        public string DeliveryLastMonthDelay { get; set; }
        public string DocReturnLastMonthDelay { get; set; }
        public string TenderLastMonthPending { get; set; }
        public string AcceptLastMonthPending { get; set; }
        public string InboundLastMonthPending { get; set; }
        public string OutboundLastMonthPending { get; set; }
        public string DeliveryLastMonthPending { get; set; }
        public string DocReturnLastMonthPending { get; set; }
        public string TenderLastMonthExclude { get; set; }
        public string AcceptLastMonthExclude { get; set; }
        public string InboundLastMonthExclude { get; set; }
        public string OutboundLastMonthExclude { get; set; }
        public string DeliveryLastMonthExclude { get; set; }
        public string DocReturnLastMonthExclude { get; set; }

        public string DNLastMonthPercent { get; set; }
        public string TenderLastMonthPercent { get; set; }
        public string AcceptLastMonthPercent { get; set; }
        public string InboundLastMonthPercent { get; set; }
        public string OutboundLastMonthPercent { get; set; }
        public string DeliveryLastMonthPercent { get; set; }
        public string DocReturnLastMonthPercent { get; set; }

        public void Initialized()
        {
            BaseBs objBs = new BaseBs();
            daysDIff = Convert.ToInt32((DateTime.Now - Convert.ToDateTime("01/01/2016")).TotalDays).ToString();
            DN01 = objBs.HomeKPIBs.GetMonth(1).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN02 = objBs.HomeKPIBs.GetMonth(2).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN03 = objBs.HomeKPIBs.GetMonth(3).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN04 = objBs.HomeKPIBs.GetMonth(4).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN05 = objBs.HomeKPIBs.GetMonth(5).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN06 = objBs.HomeKPIBs.GetMonth(6).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN07 = objBs.HomeKPIBs.GetMonth(7).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN08 = objBs.HomeKPIBs.GetMonth(8).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN09 = objBs.HomeKPIBs.GetMonth(9).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN10 = objBs.HomeKPIBs.GetMonth(10).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN11 = objBs.HomeKPIBs.GetMonth(11).ToList().Select(x => x.DNLastMonthCount).Sum();
            DN12 = objBs.HomeKPIBs.GetMonth(12).ToList().Select(x => x.DNLastMonthCount).Sum();

            SH01 = objBs.HomeKPIBs.GetMonth(1).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH02 = objBs.HomeKPIBs.GetMonth(2).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH03 = objBs.HomeKPIBs.GetMonth(3).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH04 = objBs.HomeKPIBs.GetMonth(4).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH05 = objBs.HomeKPIBs.GetMonth(5).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH06 = objBs.HomeKPIBs.GetMonth(6).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH07 = objBs.HomeKPIBs.GetMonth(7).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH08 = objBs.HomeKPIBs.GetMonth(8).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH09 = objBs.HomeKPIBs.GetMonth(9).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH10 = objBs.HomeKPIBs.GetMonth(10).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH11 = objBs.HomeKPIBs.GetMonth(11).ToList().Select(x => x.shipmentLastMonthCount).Sum();
            SH12 = objBs.HomeKPIBs.GetMonth(12).ToList().Select(x => x.shipmentLastMonthCount).Sum();

            Year = DateTime.Now.Year.ToString();
            DNLastMonthCount = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.DNLastMonthCount).Sum().ToString();
            shipmentLastMonthCount = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.shipmentLastMonthCount).Sum().ToString();
            DNThisMonthCount = objBs.HomeKPIBs.GetMonth(DateTime.Now.Month).ToList().Select(x => x.DNLastMonthCount).Sum().ToString();
            shipmentThisMonthCount = objBs.HomeKPIBs.GetMonth(DateTime.Now.Month).ToList().Select(x => x.shipmentLastMonthCount).Sum().ToString();

            AcceptLastMonthCount = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.AcceptLastMonthCount).Sum().ToString();
            AcceptLastMonthPending = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.AcceptPendingCount).Sum().ToString();
            AcceptLastMonthOntime = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.AcceptOntimeCount).Sum().ToString();
            AcceptLastMonthDelay = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.AcceptDelayCount).Sum().ToString();
            AcceptLastMonthPercent = ((Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.AcceptOntimeCount).Sum()) / Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.AcceptLastMonthCount).Sum())) * 100).ToString();

            TenderLastMonthCount = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.TenderLastMonthCount).Sum().ToString();
            TenderLastMonthPending = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.TenderPendingCount).Sum().ToString();
            TenderLastMonthOntime = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.TenderOntimeCount).Sum().ToString();
            TenderLastMonthDelay = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.TenderDelayCount).Sum().ToString();
            TenderLastMonthPercent = ((Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.TenderOntimeCount).Sum()) / Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.TenderLastMonthCount).Sum())) * 100).ToString();

            DeliveryLastMonthCount = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.DeliveryLastMonthCount).Sum().ToString();
            DeliveryLastMonthPending = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.DeliveryPendingCount).Sum().ToString();
            DeliveryLastMonthOntime = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.DeliveryOntimeCount).Sum().ToString();
            DeliveryLastMonthDelay = objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.DeliveryDelayCount).Sum().ToString();
            DeliveryLastMonthPercent = ((Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.DeliveryOntimeCount).Sum()) / Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().ToList().Select(x => x.DeliveryLastMonthCount).Sum())) * 100).ToString();
        }

    }
}