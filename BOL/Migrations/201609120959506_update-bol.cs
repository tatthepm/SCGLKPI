namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatebol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcceptedAdjusteds", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.AcceptedAdjusteds", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.AcceptedDelays", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.AcceptedDelays", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.AcceptPendings", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.AcceptPendings", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.DocReturnAdjusteds", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.DocReturnAdjusteds", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.DocReturnDelays", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.DocReturnDelays", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.DocReturnPendings", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.DocReturnPendings", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.InboundAdjusteds", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.InboundAdjusteds", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.InboundDelays", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.InboundDelays", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.InboundPendings", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.InboundPendings", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OntimeAdjusteds", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OntimeAdjusteds", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OntimeDelays", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OntimeDelays", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OntimePendings", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OntimePendings", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OutboundAdjusteds", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OutboundAdjusteds", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OutboundDelays", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OutboundDelays", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OutboundPendings", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OutboundPendings", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.TenderedAdjusteds", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.TenderedAdjusteds", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.TenderedDelays", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.TenderedDelays", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.TenderPendings", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.TenderPendings", "TRUCK_TYPE", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderPendings", "TRUCK_TYPE");
            DropColumn("dbo.TenderPendings", "SHPPOINT");
            DropColumn("dbo.TenderedDelays", "TRUCK_TYPE");
            DropColumn("dbo.TenderedDelays", "SHPPOINT");
            DropColumn("dbo.TenderedAdjusteds", "TRUCK_TYPE");
            DropColumn("dbo.TenderedAdjusteds", "SHPPOINT");
            DropColumn("dbo.OutboundPendings", "TRUCK_TYPE");
            DropColumn("dbo.OutboundPendings", "SHPPOINT");
            DropColumn("dbo.OutboundDelays", "TRUCK_TYPE");
            DropColumn("dbo.OutboundDelays", "SHPPOINT");
            DropColumn("dbo.OutboundAdjusteds", "TRUCK_TYPE");
            DropColumn("dbo.OutboundAdjusteds", "SHPPOINT");
            DropColumn("dbo.OntimePendings", "TRUCK_TYPE");
            DropColumn("dbo.OntimePendings", "SHPPOINT");
            DropColumn("dbo.OntimeDelays", "TRUCK_TYPE");
            DropColumn("dbo.OntimeDelays", "SHPPOINT");
            DropColumn("dbo.OntimeAdjusteds", "TRUCK_TYPE");
            DropColumn("dbo.OntimeAdjusteds", "SHPPOINT");
            DropColumn("dbo.InboundPendings", "TRUCK_TYPE");
            DropColumn("dbo.InboundPendings", "SHPPOINT");
            DropColumn("dbo.InboundDelays", "TRUCK_TYPE");
            DropColumn("dbo.InboundDelays", "SHPPOINT");
            DropColumn("dbo.InboundAdjusteds", "TRUCK_TYPE");
            DropColumn("dbo.InboundAdjusteds", "SHPPOINT");
            DropColumn("dbo.DocReturnPendings", "TRUCK_TYPE");
            DropColumn("dbo.DocReturnPendings", "SHPPOINT");
            DropColumn("dbo.DocReturnDelays", "TRUCK_TYPE");
            DropColumn("dbo.DocReturnDelays", "SHPPOINT");
            DropColumn("dbo.DocReturnAdjusteds", "TRUCK_TYPE");
            DropColumn("dbo.DocReturnAdjusteds", "SHPPOINT");
            DropColumn("dbo.AcceptPendings", "TRUCK_TYPE");
            DropColumn("dbo.AcceptPendings", "SHPPOINT");
            DropColumn("dbo.AcceptedDelays", "TRUCK_TYPE");
            DropColumn("dbo.AcceptedDelays", "SHPPOINT");
            DropColumn("dbo.AcceptedAdjusteds", "TRUCK_TYPE");
            DropColumn("dbo.AcceptedAdjusteds", "SHPPOINT");
        }
    }
}
