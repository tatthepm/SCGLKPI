﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BOL {
    public class SCGLKPIDbContext : DbContext {
        public DbSet<TUser> Tusers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<DOM_SAP_SHPPOINT> DOM_SAP_SHPPOINT { get; set; }
        public DbSet<DOM_CMD_VENDOR> DOM_CMD_VENDORs { get; set; }
        public DbSet<DOM_MDS_ORGANIZATION> DOM_MDS_ORGANIZATIONs { get; set; }
        public DbSet<DOM_SAP_MATFRIGRP> DOM_SAP_MATFRIGRPs { get; set; }
        public DbSet<DWH_ONTIME_DN> DWH_ONTIME_DNs { get; set; }
        public DbSet<DWH_ONTIME_SHIPMENT> DWH_ONTIME_SHIPMENTs { get; set; }
        public DbSet<KPI> KPIs { get; set; }
        public DbSet<HomeKPI> HomeKPIs { get; set; }
        public DbSet<MenuTable> MenuTables { get; set; }
        public DbSet<KPIFrequency> KPIFrequencies { get; set; }
        public DbSet<EquipmentTypes> EquipmentTypes { get; set; }
        public DbSet<HubMasters> HubMasters { get; set; }
        public DbSet<OntimeReports> OntimeReports { get; set; }
        public DbSet<OperationSummaryDaily> OperationSummaryDaily { get; set; }
        public DbSet<SaleSummaryDaily> SaleSummaryDaily { get; set; }
        public DbSet<CarrierSummaryDaily> CarrierSummaryDaily { get; set; }
        //tendered
        public DbSet<OntimeTender> OntimeTenders { get; set; }
        public DbSet<OntimeTenderMonth> OntimeTenderMonths { get; set; }
        public DbSet<OntimeTenderYear> OntimeTenderYears { get; set; }
        public DbSet<ReasonTendered> ReasonTendereds { get; set; }
        public DbSet<TenderedDelay> TenderedDelays { get; set; }
        public DbSet<TenderPending> TenderPendings { get; set; }
        public DbSet<TenderedAdjusted> TenderedAdjusted { get; set; }
        public DbSet<TenderedFiles> TenderedFiles { get; set; }
        //accepted
        public DbSet<OntimeAccept> OntimeAccepts { get; set; }
        public DbSet<OntimeAcceptMonth> OntimeAcceptMonths { get; set; }
        public DbSet<OntimeAcceptYear> OntimeAcceptYears { get; set; }
        public DbSet<AcceptedDelay> AcceptedDelays { get; set; }
        public DbSet<ReasonAccepted> ReasonAccepteds { get; set; }
        public DbSet<AcceptPending> AcceptPendings { get; set; }
        public DbSet<AcceptedAdjusted> AcceptedAdjusted { get; set; }
        public DbSet<AcceptedFiles> AcceptedFiles { get; set; }
        //inbounded
        public DbSet<OntimeInbound> OntimeInbounds { get; set; }
        public DbSet<OntimeInboundMonth> OntimeInboundMonths { get; set; }
        public DbSet<OntimeInboundYear> OntimeInboundYears { get; set; }
        public DbSet<InboundDelay> InboundDelays { get; set; }
        public DbSet<ReasonInbound> ReasonInbounds { get; set; }
        public DbSet<InboundPending> InboundPendings { get; set; }
        public DbSet<InboundAdjusted> InboundAdjusted { get; set; }
        public DbSet<InboundedFiles> InboundedFiles { get; set; }
        //outbound
        public DbSet<OntimeOutbound> OntimeOutbounds { get; set; }
        public DbSet<OntimeOutboundMonth> OntimeOutboundMonths { get; set; }
        public DbSet<OntimeOutboundYear> OntimeOutboundYears { get; set; }
        public DbSet<OutboundDelay> OutboundDelays { get; set; }
        public DbSet<ReasonOutbound> ReasonOutbounds { get; set; }
        public DbSet<OutboundPending> OutboundPendings { get; set; }
        public DbSet<OutboundAdjusted> OutboundAdjusted { get; set; }
        public DbSet<OutboundedFiles> OutboundedFiles { get; set; }
        //doc return
        public DbSet<OntimeDocReturn> OntimeDocReturns { get; set; }
        public DbSet<OntimeDocReturnMonth> OntimeDocReturnMonths { get; set; }
        public DbSet<OntimeDocReturnYear> OntimeDocReturnYears { get; set; }
        public DbSet<DocReturnDelay> DocReturnDelays { get; set; }
        public DbSet<ReasonDocReturn> ReasonDocReturns { get; set; }
        public DbSet<DocReturnPending> DocReturnPendings { get; set; }
        public DbSet<DocReturnAdjusted> DocReturnAdjusted { get; set; }
        public DbSet<DocReturnFiles> DocReturnFiles { get; set; }
        //delivery
        public DbSet<OntimeDelivery> OntimeDeliveries { get; set; }
        public DbSet<OntimeDeliveryMonth> OntimeDeliveryMonths { get; set; }
        public DbSet<OntimeDeliveryYear> OntimeDeliveryYears { get; set; }
        public DbSet<OntimeDelay> OntimeDelays { get; set; }
        public DbSet<ReasonOntime> ReasonOntimes { get; set; }
        public DbSet<OntimePending> OntimePendings { get; set; }
        public DbSet<OntimeAdjusted> OntimeAdjusted { get; set; }
        public DbSet<OntimeFiles> OntimeFiles { get; set; }

        //Monthly Masters
        public DbSet<TenderUsers> TenderUsers { get; set; }
        public DbSet<ShippingPoints> ShippingPoints { get; set; }
        public DbSet<MatFreightGroups> MatFreightGroups { get; set; }
        public DbSet<Carriers> Carriers { get; set; }

    }
}
