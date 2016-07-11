namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedDB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AcceptedPendings", newName: "AcceptedAdjusteds");
            CreateTable(
                "dbo.AcceptPendings",
                c => new
                    {
                        SHPMNTNO = c.String(nullable: false, maxLength: 10),
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
                        PLNACPDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNACPDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SHPMNTNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID);
            
            CreateTable(
                "dbo.DocReturnAdjusteds",
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
                        DEPARTMENT_Name = c.String(maxLength: 100),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SHIPTO = c.String(maxLength: 10),
                        TO_SHPG_LOC_NAME = c.String(maxLength: 280),
                        PLNDOCRETDATE_SCGL = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNDOCRETDATE_SCGL_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        DOCRETDATE_SCGL = c.DateTime(precision: 7, storeType: "datetime2"),
                        DOCRETDATE_SCGL_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID)
                .Index(t => t.DOCRETDATE_SCGL_D);
            
            CreateTable(
                "dbo.InboundAdjusteds",
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
                        DEPARTMENT_Name = c.String(maxLength: 100),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SHIPTO = c.String(maxLength: 10),
                        TO_SHPG_LOC_NAME = c.String(maxLength: 280),
                        PLNINBDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNINBDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACTGIDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACTGIDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID)
                .Index(t => t.ACTGIDATE_D);
            
            CreateTable(
                "dbo.OntimeAdjusteds",
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
                        DEPARTMENT_Name = c.String(maxLength: 100),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SHIPTO = c.String(maxLength: 10),
                        TO_SHPG_LOC_NAME = c.String(maxLength: 280),
                        PLNONTIMEDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNONTIMEDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACDLVDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACDLVDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID)
                .Index(t => t.ACDLVDATE_D);
            
            CreateTable(
                "dbo.OutboundAdjusteds",
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
                        DEPARTMENT_Name = c.String(maxLength: 100),
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
            
            CreateTable(
                "dbo.TenderedAdjusteds",
                c => new
                    {
                        SHPMNTNO = c.String(nullable: false, maxLength: 10),
                        CARRIER_ID = c.String(maxLength: 32),
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
                        SEGMENT = c.String(maxLength: 20),
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
            
            AddColumn("dbo.AcceptedAdjusteds", "CARRIER_ID", c => c.String(maxLength: 32));
            AddColumn("dbo.AcceptedAdjusteds", "VENDOR_CODE", c => c.String(maxLength: 40));
            AddColumn("dbo.AcceptedAdjusteds", "VENDOR_NAME", c => c.String(maxLength: 140));
            AddColumn("dbo.AcceptedAdjusteds", "LACPDDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AcceptedAdjusteds", "LACPDDATE_D", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.AcceptedAdjusteds", "LACPDDATE_D");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TenderedAdjusteds", new[] { "FTNRDDATE_D" });
            DropIndex("dbo.TenderedAdjusteds", new[] { "SECTION_ID" });
            DropIndex("dbo.TenderedAdjusteds", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.TenderedAdjusteds", new[] { "MATFRIGRP" });
            DropIndex("dbo.OutboundAdjusteds", new[] { "ACDLVDATE_D" });
            DropIndex("dbo.OutboundAdjusteds", new[] { "SECTION_ID" });
            DropIndex("dbo.OutboundAdjusteds", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.OutboundAdjusteds", new[] { "MATFRIGRP" });
            DropIndex("dbo.OntimeAdjusteds", new[] { "ACDLVDATE_D" });
            DropIndex("dbo.OntimeAdjusteds", new[] { "SECTION_ID" });
            DropIndex("dbo.OntimeAdjusteds", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.OntimeAdjusteds", new[] { "MATFRIGRP" });
            DropIndex("dbo.InboundAdjusteds", new[] { "ACTGIDATE_D" });
            DropIndex("dbo.InboundAdjusteds", new[] { "SECTION_ID" });
            DropIndex("dbo.InboundAdjusteds", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.InboundAdjusteds", new[] { "MATFRIGRP" });
            DropIndex("dbo.DocReturnAdjusteds", new[] { "DOCRETDATE_SCGL_D" });
            DropIndex("dbo.DocReturnAdjusteds", new[] { "SECTION_ID" });
            DropIndex("dbo.DocReturnAdjusteds", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.DocReturnAdjusteds", new[] { "MATFRIGRP" });
            DropIndex("dbo.AcceptPendings", new[] { "SECTION_ID" });
            DropIndex("dbo.AcceptPendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.AcceptPendings", new[] { "MATFRIGRP" });
            DropIndex("dbo.AcceptedAdjusteds", new[] { "LACPDDATE_D" });
            DropColumn("dbo.AcceptedAdjusteds", "LACPDDATE_D");
            DropColumn("dbo.AcceptedAdjusteds", "LACPDDATE");
            DropColumn("dbo.AcceptedAdjusteds", "VENDOR_NAME");
            DropColumn("dbo.AcceptedAdjusteds", "VENDOR_CODE");
            DropColumn("dbo.AcceptedAdjusteds", "CARRIER_ID");
            DropTable("dbo.TenderedAdjusteds");
            DropTable("dbo.OutboundAdjusteds");
            DropTable("dbo.OntimeAdjusteds");
            DropTable("dbo.InboundAdjusteds");
            DropTable("dbo.DocReturnAdjusteds");
            DropTable("dbo.AcceptPendings");
            RenameTable(name: "dbo.AcceptedAdjusteds", newName: "AcceptedPendings");
        }
    }
}
