using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SCGLKPIUI {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            #region 1 on-time accepted tendered
            // accepted daily
            routes.MapRoute("OntimeAcceptDaily", "OntimeAcceptedDailyChart/OntimeAcceptTableDaily/",
                new { controller = "OntimeAcceptedDailyChart", action = "OntimeAcceptTableDaily" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            // accepted summary daily
            routes.MapRoute("OntimeAcceptSummaryDaily", "OntimeAcceptedDailyChart/OntimeAcceptSummaryDaily/",
                new { controller = "OntimeAcceptedDailyChart", action = "OntimeAcceptSummaryDaily" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            // accepted monthly
            routes.MapRoute("OntimeAcceptMonthly", "OntimeAcceptedMonthlyChart/OntimeAcceptTableMonthly/",
                new { controller = "OntimeAcceptedMonthlyChart", action = "OntimeAcceptTableMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            // accepted summary monthly
            routes.MapRoute("OntimeAcceptSummaryMonthly", "OntimeAcceptedMonthlyChart/OntimeAcceptTableSummaryMonthly/",
                new { controller = "OntimeAcceptedMonthlyChart", action = "OntimeAcceptTableSummaryMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //5 accept yearly
            routes.MapRoute("OntimeAcceptedYearly", "OntimeAcceptedYearlyChart/OntimeAcceptedTableYearly/",
                new { controller = "OntimeAcceptedYearlyChart", action = "OntimeAcceptedTableYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //6 accept summary yearly
            routes.MapRoute("OntimeAcceptedSummaryYearly", "OntimeAcceptedYearlyChart/OntimeAcceptedTableSummaryYearly/",
                new { controller = "OntimeAcceptedYearlyChart", action = "OntimeAcceptedTableSummaryYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //7 doc pending
            routes.MapRoute("PendingAcceptTableSummary", "PendingAccept/PendingAcceptTableSummary/",
                new { controller = "PendingAccept", action = "PendingAcceptTableSummary" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //8 adjust accept
            routes.MapRoute("JsonAdjustAcceptTable", "AdjustAccepted/JsonAdjustAcceptTable/",
                new { controller = "AdjustAccepted", action = "JsonAdjustAcceptTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //9 approve accept
            routes.MapRoute("JsonApproveAcceptTable", "ApproveAccepted/JsonApproveAcceptTable/",
                new { controller = "ApproveAccepted", action = "JsonApproveAcceptTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            #endregion

            #region 2 on-time tendered
            //1
            routes.MapRoute("OntimeTenderedDaily", "OntimeTenderedDailyChart/OntimeTenderedTableDaily/",
               new { controller = "OntimeTenderedDailyChart", action = "OntimeTenderedTableDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //2
            routes.MapRoute("OntimeTenderedSummaryDaily", "OntimeTenderedDailyChart/OntimeTenderedSummaryDaily/",
               new { controller = "OntimeTenderedDailyChart", action = "OntimeTenderedSummaryDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            // tendered monthly
            routes.MapRoute("OntimeTenderedMonthly", "OntimeTenderedMonthlyChart/OntimeTenderedTableMonthly/",
                new { controller = "OntimeTenderedMonthlyChart", action = "OntimeTenderedTableMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            // tendered summary monthly
            routes.MapRoute("OntimeTenderedSummaryMonthly", "OntimeTenderedMonthlyChart/OntimeTenderedTableSummaryMonthly/",
                new { controller = "OntimeTenderedMonthlyChart", action = "OntimeTenderedTableSummaryMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //5 tender yearly
            routes.MapRoute("OntimeTenderYearly", "OntimeTenderedYearlyChart/OntimeTenderedTableYearly/",
                new { controller = "OntimeTenderedYearlyChart", action = "OntimeTenderedTableYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //6 doc return summary yearly
            routes.MapRoute("OntimeTenderSummaryYearly", "OntimeTenderedYearlyChart/OntimeTenderedTableSummaryYearly/",
                new { controller = "OntimeTenderedYearlyChart", action = "OntimeTenderedTableSummaryYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //7 doc pending
            routes.MapRoute("PendingTenderTableSummary", "PendingTender/PendingTenderTableSummary/",
                new { controller = "PendingTender", action = "PendingTenderTableSummary" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //8 adjust tender
            routes.MapRoute("JsonAdjustTenderTable", "AdjustTendered/JsonAdjustTenderTable/",
                new { controller = "AdjustTendered", action = "JsonAdjustTenderTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //9 approve tender
            routes.MapRoute("JsonApproveTenderTable", "ApproveTendered/JsonApproveTenderTable/",
                new { controller = "ApproveTendered", action = "JsonApproveTenderTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            #endregion

            #region 3 on-time inbounded
            //1
            routes.MapRoute("OntimeInboundDaily", "OntimeInboundedDailyChart/OntimeInboundTableDaily/",
               new { controller = "OntimeInboundedDailyChart", action = "OntimeInboundTableDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //2
            routes.MapRoute("OntimeInboundSummaryDaily", "OntimeInboundedDailyChart/OntimeInboundSummaryDaily/",
               new { controller = "OntimeInboundSummaryDaily", action = "OntimeInboundSummaryDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //3 inbound monthly
            routes.MapRoute("OntimeInboundedMonthly", "OntimeInboundedMonthlyChart/OntimeInboundTableMonthly/",
                new { controller = "OntimeInboundedMonthlyChart", action = "OntimeInboundTableMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //4 inbound summary monthly
            routes.MapRoute("OntimeInboundedSummaryMonthly", "OntimeInboundedMonthlyChart/OntimeInboundTableSummaryMonthly/",
                new { controller = "OntimeInboundedSummaryMonthly", action = "OntimeInboundTableSummaryMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //5 inbound yearly
            routes.MapRoute("OntimeInboundedYearly", "OntimeInboundedYearlyChart/OntimeInboundedTableYearly/",
                new { controller = "OntimeInboundedYearlyChart", action = "OntimeInboundedTableYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //6 inbound summary yearly
            routes.MapRoute("OntimeInboundedSummaryYearly", "OntimeInboundedYearlyChart/OntimeInboundedTableSummaryYearly/",
                new { controller = "OntimeInboundedYearlyChart", action = "OntimeInboundedTableSummaryYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //7 doc pending
            routes.MapRoute("PendingInboundTableSummary", "PendingInbound/PendingInboundTableSummary/",
                new { controller = "PendingInbound", action = "PendingInboundTableSummary" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //8 adjust Inbound
            routes.MapRoute("JsonAdjustInboundTable", "AdjustInbounded/JsonAdjustInboundTable/",
                new { controller = "AdjustInbounded", action = "JsonAdjustInboundTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //9 approve Inbound
            routes.MapRoute("JsonApproveInboundTable", "ApproveInbounded/JsonApproveInboundTable/",
                new { controller = "ApproveInbounded", action = "JsonApproveInboundTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            #endregion

            #region 4 on-time outbound
            //1
            routes.MapRoute("OntimeOutboundDaily", "OntimeOutboundedDailyChart/OntimeOutboundedTableDaily/",
               new { controller = "OntimeOutboundedDailyChart", action = "OntimeOutboundedTableDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //2
            routes.MapRoute("OntimeOutboundedSummaryDaily", "OntimeOutboundedDailyChart/OntimeOutboundedSummaryDaily/",
               new { controller = "OntimeOutboundedDailyChart", action = "OntimeOutboundedSummaryDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //3 outbound monthly
            routes.MapRoute("OntimeOutboundedMonthly", "OntimeOutboundedMonthlyChart/OntimeOutboundTableMonthly/",
                new { controller = "OntimeOutboundedMonthlyChart", action = "OntimeOutboundTableMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //4 inbound summary monthly
            routes.MapRoute("OntimeOutboundedSummaryMonthly", "OntimeOutboundedMonthlyChart/OntimeOutboundTableSummaryMonthly/",
                new { controller = "OntimeOutboundedMonthlyChart", action = "OntimeOutboundTableSummaryMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //5 outbound yearly
            routes.MapRoute("OntimeOutboundedYearly", "OntimeOutbounedYearlyChart/OntimeOutboundedTableYearly/",
                new { controller = "OntimeOutbounedYearlyChart", action = "OntimeOutboundedTableYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //6 outbound summary yearly
            routes.MapRoute("OntimeOutboundedSummaryYearly", "OntimeOutbounedYearlyChart/OntimeOutboundedTableSummaryYearly/",
               new { controller = "OntimeOutbounedYearlyChart", action = "OntimeOutboundedTableSummaryYearly" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //7 doc pending
            routes.MapRoute("PendingOutboundTableSummary", "PendingOutbound/PendingOutboundTableSummary/",
                new { controller = "PendingOutbound", action = "PendingOutboundTableSummary" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //8 adjust Outbound
            routes.MapRoute("JsonAdjustOutboundTable", "AdjustOutbounded/JsonAdjustOutboundTable/",
                new { controller = "AdjustOutbounded", action = "JsonAdjustOutboundTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //9 approve Outbound
            routes.MapRoute("JsonApproveOutboundTable", "ApproveOutbounded/JsonApproveOutboundTable/",
                new { controller = "ApproveOutbounded", action = "JsonApproveOutboundTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });
            #endregion

            #region 5 on-time document return
            //1
            routes.MapRoute("OntimeDocReturnedDaily", "OntimeDocReturnedDailyChart/OntimeDocReturnTableDaily/",
               new { controller = "OntimeDocReturnedDailyChart", action = "OntimeDocReturnTableDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //2
            routes.MapRoute("OntimeDocReturnSummaryDaily", "OntimeDocReturnedDailyChart/OntimeDocReturnSummaryDaily/",
               new { controller = "OntimeDocReturnedDailyChart", action = "OntimeDocReturnSummaryDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //3 outbound monthly
            routes.MapRoute("OntimeDocReturnedMonthly", "OntimeDocReturnedMonthlyChart/OntimeDocReturnTableMonthly/",
                new { controller = "OntimeDocReturnedMonthlyChart", action = "OntimeDocReturnTableMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //4 doc return summary monthly
            routes.MapRoute("OntimeDocReturnedSummaryMonthly", "OntimeDocReturnedMonthlyChart/OntimeDocReturnTableSummaryMonthly/",
                new { controller = "OntimeDocReturnedMonthlyChart", action = "OntimeDocReturnTableSummaryMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //5 doc return yearly
            routes.MapRoute("OntimeDocReturnYearly", "OntimeDocReturnedYearlyChart/OntimeDocReturnedTableYearly/",
                new { controller = "OntimeDocReturnedYearlyChart", action = "OntimeDocReturnedTableYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //6 doc return summary yearly
            routes.MapRoute("OntimeDocReturnSummaryYearly", "OntimeDocReturnedYearlyChart/OntimeDocReturnedTableSummaryYearly/",
                new { controller = "OntimeDocReturnedYearlyChart", action = "OntimeDocReturnedTableSummaryYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //7 doc pending
            routes.MapRoute("PendingDocReturnTableSummary", "PendingDocReturn/PendingDocReturnTableSummary/",
                new { controller = "PendingDocReturn", action = "PendingDocReturnTableSummary" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //8 adjust DocReturn
            routes.MapRoute("JsonAdjustDocReturnTable", "AdjustDocReturned/JsonAdjustDocReturnTable/",
                new { controller = "AdjustDocReturned", action = "JsonAdjustDocReturnTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //9 approve DocReturn
            routes.MapRoute("JsonApproveDocReturnTable", "ApproveDocReturned/JsonApproveDocReturnTable/",
                new { controller = "ApproveDocReturned", action = "JsonApproveDocReturnTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });
            #endregion

            #region 6 on-time delivery
            //1
            routes.MapRoute("OntimeDeliveryDaily", "OntimeDeliveredDailyChart/OntimeDeliveredTableDaily/",
               new { controller = "OntimeDeliveredDailyChart", action = "OntimeDeliveredTableDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //2
            routes.MapRoute("OntimeDeliverySummaryDaily", "OntimeDeliveredDailyChart/OntimeDeliveredSummaryDaily/",
               new { controller = "OntimeDeliveredDailyChart", action = "OntimeDeliveredSummaryDaily" },
               new[] { "JsonRenderingMvcApplication.Controllers" });

            //3 outbound monthly
            routes.MapRoute("OntimeDeliveredMonthly", "OntimeDeliveredMonthlyChart/OntimeDeliveredTableMonthly/",
                new { controller = "OntimeDeliveredMonthlyChart", action = "OntimeDeliveredTableMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //4 doc return summary monthly
            routes.MapRoute("OntimeDeliveredSummaryMonthly", "OntimeDeliveredMonthlyChart/OntimeDeliveredTableSummaryMonthly/",
                new { controller = "OntimeDeliveredMonthlyChart", action = "OntimeDeliveredTableSummaryMonthly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //5 doc return yearly
            routes.MapRoute("OntimeDeliveredYearly", "OntimeDeliveredYearlyChart/OntimeDeliveredTableYearly/",
                new { controller = "OntimeDeliveredYearlyChart", action = "OntimeDeliveredTableYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //6 doc return summary yearly
            routes.MapRoute("OntimeDeliveredSummaryYearly", "OntimeDeliveredYearlyChart/OntimeDeliveredTableSummaryYearly/",
                new { controller = "OntimeDeliveredYearlyChart", action = "OntimeDeliveredTableSummaryYearly" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //7 doc pending
            routes.MapRoute("PendingDeliveryTableSummary", "PendingDelivery/PendingDeliveryTableSummary/",
                new { controller = "PendingDelivery", action = "PendingDeliveryTableSummary" },
                new[] { "JsonRenderingMvcApplication.Controllers" });
            
            //8 adjust Deliver
            routes.MapRoute("JsonAdjustOntimeTable", "AdjustDelivered/JsonAdjustOntimeTable/",
                new { controller = "AdjustDelivered", action = "JsonAdjustOntimeTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });

            //9 approve Deliver
            routes.MapRoute("JsonApproveOntimeTable", "ApproveDelivered/JsonApproveOntimeTable/",
                new { controller = "ApproveDelivered", action = "JsonApproveOntimeTable" },
                new[] { "JsonRenderingMvcApplication.Controllers" });
            #endregion
        }
    }
}
