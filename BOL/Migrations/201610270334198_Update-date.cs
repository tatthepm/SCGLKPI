namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OntimePendings", new[] { "PLNONTIMEDATE_D" });
            DropIndex("dbo.OutboundPendings", new[] { "PLNOUTBDATE_D" });
            AddColumn("dbo.AcceptedAdjusteds", "LTNRDDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AcceptedAdjusteds", "LTNRDDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AcceptedDelays", "LTNRDDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AcceptedDelays", "LTNRDDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AcceptPendings", "LTNRDDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AcceptPendings", "LTNRDDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DocReturnPendings", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DocReturnPendings", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundPendings", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundPendings", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAdjusteds", "SHCRDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAdjusteds", "ORDCMPDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAdjusteds", "REQUESTED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeDelays", "SHCRDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeDelays", "ORDCMPDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeDelays", "REQUESTED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimePendings", "SHCRDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimePendings", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimePendings", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimePendings", "ORDCMPDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimePendings", "REQUESTED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OutboundPendings", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OutboundPendings", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TenderedAdjusteds", "SHCRDATE", c => c.DateTime());
            AddColumn("dbo.TenderedAdjusteds", "SHCRDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TenderedDelays", "SHCRDATE", c => c.DateTime());
            AddColumn("dbo.TenderedDelays", "SHCRDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TenderPendings", "SHCRDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.InboundPendings", "ACTGIDATE_D");
        }
        
        public override void Down()
        {
            DropIndex("dbo.InboundPendings", new[] { "ACTGIDATE_D" });
            DropColumn("dbo.TenderPendings", "SHCRDATE_D");
            DropColumn("dbo.TenderedDelays", "SHCRDATE_D");
            DropColumn("dbo.TenderedDelays", "SHCRDATE");
            DropColumn("dbo.TenderedAdjusteds", "SHCRDATE_D");
            DropColumn("dbo.TenderedAdjusteds", "SHCRDATE");
            DropColumn("dbo.OutboundPendings", "ACTGIDATE_D");
            DropColumn("dbo.OutboundPendings", "ACTGIDATE");
            DropColumn("dbo.OntimePendings", "REQUESTED_DATE");
            DropColumn("dbo.OntimePendings", "ORDCMPDATE");
            DropColumn("dbo.OntimePendings", "ACTGIDATE_D");
            DropColumn("dbo.OntimePendings", "ACTGIDATE");
            DropColumn("dbo.OntimePendings", "SHCRDATE");
            DropColumn("dbo.OntimeDelays", "REQUESTED_DATE");
            DropColumn("dbo.OntimeDelays", "ORDCMPDATE");
            DropColumn("dbo.OntimeDelays", "SHCRDATE");
            DropColumn("dbo.OntimeAdjusteds", "REQUESTED_DATE");
            DropColumn("dbo.OntimeAdjusteds", "ORDCMPDATE");
            DropColumn("dbo.OntimeAdjusteds", "SHCRDATE");
            DropColumn("dbo.InboundPendings", "ACTGIDATE_D");
            DropColumn("dbo.InboundPendings", "ACTGIDATE");
            DropColumn("dbo.DocReturnPendings", "ACTGIDATE_D");
            DropColumn("dbo.DocReturnPendings", "ACTGIDATE");
            DropColumn("dbo.AcceptPendings", "LTNRDDATE_D");
            DropColumn("dbo.AcceptPendings", "LTNRDDATE");
            DropColumn("dbo.AcceptedDelays", "LTNRDDATE_D");
            DropColumn("dbo.AcceptedDelays", "LTNRDDATE");
            DropColumn("dbo.AcceptedAdjusteds", "LTNRDDATE_D");
            DropColumn("dbo.AcceptedAdjusteds", "LTNRDDATE");
            CreateIndex("dbo.OutboundPendings", "PLNOUTBDATE_D");
            CreateIndex("dbo.OntimePendings", "PLNONTIMEDATE_D");
        }
    }
}
