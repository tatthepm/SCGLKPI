namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTenderedDelay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TenderedDelays",
                c => new
                    {
                        SHPMNTNO = c.String(nullable: false, maxLength: 10),
                        CARRIER_ID = c.Int(),
                        VENDOR_CODE = c.String(maxLength: 40),
                        VENDOR_NAME = c.String(maxLength: 140),
                        MATFRIGRP = c.String(maxLength: 8),
                        MATNAME = c.String(maxLength: 100),
                        REGION_ID = c.String(maxLength: 5),
                        REGION_NAME_TH = c.String(maxLength: 30),
                        REGION_NAME_EN = c.String(maxLength: 30),
                        DEPARTMENT_ID = c.String(maxLength: 5),
                        DEPARTMENT_Name = c.String(maxLength: 100),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SHIPTO = c.String(maxLength: 10),
                        LAST_SHPG_LOC_NAME = c.String(maxLength: 280),
                        PLNTNRDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNTNRDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        FTNRDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        FTNRDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SHPMNTNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID)
                .Index(t => t.FTNRDDATE_D);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TenderedDelays", new[] { "FTNRDDATE_D" });
            DropIndex("dbo.TenderedDelays", new[] { "SECTION_ID" });
            DropIndex("dbo.TenderedDelays", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.TenderedDelays", new[] { "MATFRIGRP" });
            DropTable("dbo.TenderedDelays");
        }
    }
}
