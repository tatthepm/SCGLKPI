namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateActGI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocReturnAdjusteds", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DocReturnAdjusteds", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DocReturnDelays", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DocReturnDelays", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAdjusteds", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAdjusteds", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeDelays", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeDelays", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OutboundAdjusteds", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OutboundAdjusteds", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OutboundDelays", "ACTGIDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OutboundDelays", "ACTGIDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OutboundDelays", "ACTGIDATE_D");
            DropColumn("dbo.OutboundDelays", "ACTGIDATE");
            DropColumn("dbo.OutboundAdjusteds", "ACTGIDATE_D");
            DropColumn("dbo.OutboundAdjusteds", "ACTGIDATE");
            DropColumn("dbo.OntimeDelays", "ACTGIDATE_D");
            DropColumn("dbo.OntimeDelays", "ACTGIDATE");
            DropColumn("dbo.OntimeAdjusteds", "ACTGIDATE_D");
            DropColumn("dbo.OntimeAdjusteds", "ACTGIDATE");
            DropColumn("dbo.DocReturnDelays", "ACTGIDATE_D");
            DropColumn("dbo.DocReturnDelays", "ACTGIDATE");
            DropColumn("dbo.DocReturnAdjusteds", "ACTGIDATE_D");
            DropColumn("dbo.DocReturnAdjusteds", "ACTGIDATE");
        }
    }
}
