namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNewOutboundDelay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OutboundDelays",
                c => new
                    {
                        DELVNO = c.String(nullable: false, maxLength: 10),
                        CARRIER_ID = c.String(maxLength: 32),
                        VENDOR_CODE = c.String(maxLength: 40),
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
                        TO_SHPG_LOC_NAME = c.String(maxLength: 280),
                        PLNOUTBDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNOUTBDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACDLVDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACDLVDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID)
                .Index(t => t.ACDLVDATE_D);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.OutboundDelays", new[] { "ACDLVDATE_D" });
            DropIndex("dbo.OutboundDelays", new[] { "SECTION_ID" });
            DropIndex("dbo.OutboundDelays", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.OutboundDelays", new[] { "MATFRIGRP" });
            DropTable("dbo.OutboundDelays");
        }
    }
}
