namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homekpi : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HomeKPIs");
            AddColumn("dbo.HomeKPIs", "TenderOntimeCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "AcceptOntimeCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "InboundOntimeCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "OutboundOntimeCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "DeliveryOntimeCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "DocReturnOntimeCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "TenderDelayCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "AcceptDelayCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "InboundDelayCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "OutboundDelayCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "DeliveryDelayCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "DocReturnDelayCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "TenderPendingCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "AcceptPendingCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "InboundPendingCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "OutboundPendingCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "DeliveryPendingCount", c => c.Int(nullable: false));
            AddColumn("dbo.HomeKPIs", "DocReturnPendingCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "month_year", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.HomeKPIs", "daysDIff", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "LastMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "shipmentLastMonthCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "DNLastMonthCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "OntimeCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "DelayCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "PendingCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "TenderLastMonthCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "AcceptLastMonthCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "InboundLastMonthCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "OutboundLastMonthCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "DeliveryLastMonthCount", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeKPIs", "DocReturnLastMonthCount", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.HomeKPIs", "month_year");
            DropColumn("dbo.HomeKPIs", "PercentOntime");
            DropColumn("dbo.HomeKPIs", "PercentDelay");
            DropColumn("dbo.HomeKPIs", "PercentPending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HomeKPIs", "PercentPending", c => c.String(maxLength: 15));
            AddColumn("dbo.HomeKPIs", "PercentDelay", c => c.String(maxLength: 15));
            AddColumn("dbo.HomeKPIs", "PercentOntime", c => c.String(maxLength: 15));
            DropPrimaryKey("dbo.HomeKPIs");
            AlterColumn("dbo.HomeKPIs", "DocReturnLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "DeliveryLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "OutboundLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "InboundLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "AcceptLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "TenderLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "PendingCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "DelayCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "OntimeCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "DNLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "shipmentLastMonthCount", c => c.String(maxLength: 15));
            AlterColumn("dbo.HomeKPIs", "Year", c => c.String(maxLength: 4));
            AlterColumn("dbo.HomeKPIs", "LastMonth", c => c.String(maxLength: 10));
            AlterColumn("dbo.HomeKPIs", "daysDIff", c => c.String(maxLength: 10));
            AlterColumn("dbo.HomeKPIs", "month_year", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.HomeKPIs", "DocReturnPendingCount");
            DropColumn("dbo.HomeKPIs", "DeliveryPendingCount");
            DropColumn("dbo.HomeKPIs", "OutboundPendingCount");
            DropColumn("dbo.HomeKPIs", "InboundPendingCount");
            DropColumn("dbo.HomeKPIs", "AcceptPendingCount");
            DropColumn("dbo.HomeKPIs", "TenderPendingCount");
            DropColumn("dbo.HomeKPIs", "DocReturnDelayCount");
            DropColumn("dbo.HomeKPIs", "DeliveryDelayCount");
            DropColumn("dbo.HomeKPIs", "OutboundDelayCount");
            DropColumn("dbo.HomeKPIs", "InboundDelayCount");
            DropColumn("dbo.HomeKPIs", "AcceptDelayCount");
            DropColumn("dbo.HomeKPIs", "TenderDelayCount");
            DropColumn("dbo.HomeKPIs", "DocReturnOntimeCount");
            DropColumn("dbo.HomeKPIs", "DeliveryOntimeCount");
            DropColumn("dbo.HomeKPIs", "OutboundOntimeCount");
            DropColumn("dbo.HomeKPIs", "InboundOntimeCount");
            DropColumn("dbo.HomeKPIs", "AcceptOntimeCount");
            DropColumn("dbo.HomeKPIs", "TenderOntimeCount");
            AddPrimaryKey("dbo.HomeKPIs", "month_year");
        }
    }
}
