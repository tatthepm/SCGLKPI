namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpending : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcceptedPendings",
                c => new
                    {
                        SHPMNTNO = c.String(nullable: false, maxLength: 10),
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
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SHPMNTNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID);
            
            CreateTable(
                "dbo.DocReturnPendings",
                c => new
                    {
                        DELVNO = c.String(nullable: false, maxLength: 10),
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
                        PLNDOCRETDATE_SCGL = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNDOCRETDATE_SCGL_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID);
            
            CreateTable(
                "dbo.InboundPendings",
                c => new
                    {
                        DELVNO = c.String(nullable: false, maxLength: 10),
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
                        PLNINBDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNINBDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID);
            
            CreateTable(
                "dbo.OntimePendings",
                c => new
                    {
                        DELVNO = c.String(nullable: false, maxLength: 10),
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
                        PLNONTIMEDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNONTIMEDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID);
            
            CreateTable(
                "dbo.OutboundPendings",
                c => new
                    {
                        DELVNO = c.String(nullable: false, maxLength: 10),
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
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DELVNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID);
            
            CreateTable(
                "dbo.TenderPendings",
                c => new
                    {
                        SHPMNTNO = c.String(nullable: false, maxLength: 10),
                        MATFRIGRP = c.String(maxLength: 8),
                        MATNAME = c.String(maxLength: 100),
                        REGION_ID = c.String(maxLength: 5),
                        REGION_NAME_TH = c.String(maxLength: 30),
                        REGION_NAME_EN = c.String(maxLength: 30),
                        DEPARTMENT_ID = c.String(maxLength: 5),
                        DEPARTMENT_NAME = c.String(maxLength: 100),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SEGMENT = c.String(maxLength: 20),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SHIPTO = c.String(maxLength: 10),
                        LAST_SHPG_LOC_NAME = c.String(maxLength: 280),
                        PLNTNRDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNTNRDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SHPMNTNO)
                .Index(t => t.MATFRIGRP)
                .Index(t => t.DEPARTMENT_ID)
                .Index(t => t.SECTION_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TenderPendings", new[] { "SECTION_ID" });
            DropIndex("dbo.TenderPendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.TenderPendings", new[] { "MATFRIGRP" });
            DropIndex("dbo.OutboundPendings", new[] { "SECTION_ID" });
            DropIndex("dbo.OutboundPendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.OutboundPendings", new[] { "MATFRIGRP" });
            DropIndex("dbo.OntimePendings", new[] { "SECTION_ID" });
            DropIndex("dbo.OntimePendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.OntimePendings", new[] { "MATFRIGRP" });
            DropIndex("dbo.InboundPendings", new[] { "SECTION_ID" });
            DropIndex("dbo.InboundPendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.InboundPendings", new[] { "MATFRIGRP" });
            DropIndex("dbo.DocReturnPendings", new[] { "SECTION_ID" });
            DropIndex("dbo.DocReturnPendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.DocReturnPendings", new[] { "MATFRIGRP" });
            DropIndex("dbo.AcceptedPendings", new[] { "SECTION_ID" });
            DropIndex("dbo.AcceptedPendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.AcceptedPendings", new[] { "MATFRIGRP" });
            DropTable("dbo.TenderPendings");
            DropTable("dbo.OutboundPendings");
            DropTable("dbo.OntimePendings");
            DropTable("dbo.InboundPendings");
            DropTable("dbo.DocReturnPendings");
            DropTable("dbo.AcceptedPendings");
        }
    }
}
