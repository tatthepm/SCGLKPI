namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldLoaddedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DWH_ONTIME_DN", "LOADED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "LOADED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAcceptMonths", "LOADED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAccepts", "LOADED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeTenders", "LOADED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OntimeTenders", "LOADED_DATE");
            DropColumn("dbo.OntimeAccepts", "LOADED_DATE");
            DropColumn("dbo.OntimeAcceptMonths", "LOADED_DATE");
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "LOADED_DATE");
            DropColumn("dbo.DWH_ONTIME_DN", "LOADED_DATE");
        }
    }
}
