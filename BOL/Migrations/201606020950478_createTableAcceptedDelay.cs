namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTableAcceptedDelay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcceptedDelays",
                c => new
                    {
                        SHPMNTNO = c.String(nullable: false, maxLength: 10),
                        CARRIER_ID = c.Int(),
                        VENDOR_NAME = c.String(maxLength: 140),
                        MATFRIGRP = c.String(maxLength: 8),
                        MATNAME = c.String(maxLength: 100),
                        REGION_ID = c.String(maxLength: 5),
                        REGION_NAME_TH = c.String(maxLength: 30),
                        REGION_NAME_EN = c.String(maxLength: 30),
                        DEPARTMENT_ID = c.String(maxLength: 5),
                        DEPARTMENT_NAME = c.String(maxLength: 100),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SHIPTO = c.String(maxLength: 10),
                        LAST_SHPG_LOC_NAME = c.String(maxLength: 280),
                        PLNACPDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNACPDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LACPDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LACPDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SHPMNTNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID)
                .Index(t => t.LACPDDATE_D);
            
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "MATNAM", c => c.String());
        }
        
        public override void Down()
        {
            DropIndex("dbo.AcceptedDelays", new[] { "LACPDDATE_D" });
            DropIndex("dbo.AcceptedDelays", new[] { "SECTION_ID" });
            DropIndex("dbo.AcceptedDelays", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.AcceptedDelays", new[] { "MATFRIGRP" });
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "MATNAM");
            DropTable("dbo.AcceptedDelays");
        }
    }
}
