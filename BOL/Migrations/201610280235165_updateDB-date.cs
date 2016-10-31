namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDBdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InboundAdjusteds", "LTNRDDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundAdjusteds", "LTNRDDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundDelays", "LTNRDDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundDelays", "LTNRDDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundPendings", "LTNRDDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundPendings", "LTNRDDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TenderPendings", "SHCRDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderPendings", "SHCRDATE");
            DropColumn("dbo.InboundPendings", "LTNRDDATE_D");
            DropColumn("dbo.InboundPendings", "LTNRDDATE");
            DropColumn("dbo.InboundDelays", "LTNRDDATE_D");
            DropColumn("dbo.InboundDelays", "LTNRDDATE");
            DropColumn("dbo.InboundAdjusteds", "LTNRDDATE_D");
            DropColumn("dbo.InboundAdjusteds", "LTNRDDATE");
        }
    }
}
