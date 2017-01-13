namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createSCGLKPI : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DOM_CMD_VENDOR",
                c => new
                    {
                        VENDOR_CODE = c.String(nullable: false, maxLength: 40),
                        VENDOR_NAME = c.String(maxLength: 140),
                        STREET = c.String(maxLength: 200),
                        DISTRICT = c.String(maxLength: 200),
                        CITY = c.String(maxLength: 140),
                        COUNTRY = c.String(maxLength: 12),
                        POSTAL_CODE = c.String(maxLength: 40),
                        TAX_NUMBER = c.String(maxLength: 64),
                        TELEPHONE = c.String(maxLength: 64),
                        FAX = c.String(maxLength: 124),
                        COMPANY_CODE = c.String(maxLength: 4),
                        FLAG_DEL = c.String(maxLength: 1),
                        CREATE_BY = c.String(maxLength: 20),
                        CREATE_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LASTUPDATE_BY = c.String(maxLength: 20),
                        LASTUPDATE_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VENDOR_CODE);
            
            CreateTable(
                "dbo.DOM_MDS_ORGANIZATION",
                c => new
                    {
                        MATFRIGRP = c.String(nullable: false, maxLength: 8),
                        SHPPOINT_ID = c.String(nullable: false, maxLength: 4),
                        REGION_ID = c.String(nullable: false, maxLength: 4),
                        VALIDFROM = c.DateTime(nullable: false),
                        VALIDTO = c.DateTime(precision: 7, storeType: "datetime2"),
                        SHPPOINT_ID_Name = c.String(maxLength: 150),
                        REGION_ID_Name = c.String(maxLength: 100),
                        COMPCODE = c.String(maxLength: 10),
                        COMPANY_NAME = c.String(maxLength: 50),
                        DEPARTMENT_ID = c.String(maxLength: 5),
                        DEPARTMENT_Name = c.String(maxLength: 100),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        BU = c.String(maxLength: 100),
                        TYPE = c.String(maxLength: 100),
                        EnterDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        EnterUserName = c.String(maxLength: 100),
                        LastChgDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastChgUserName = c.String(maxLength: 100),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.MATFRIGRP, t.SHPPOINT_ID, t.REGION_ID, t.VALIDFROM });
            
            CreateTable(
                "dbo.DOM_SAP_MATFRIGRP",
                c => new
                    {
                        MATFRIGRP = c.String(nullable: false, maxLength: 8),
                        MATNAME = c.String(maxLength: 100),
                        WORKTYPE = c.String(maxLength: 3),
                        SUB_WORKTYPE = c.String(maxLength: 3),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.MATFRIGRP);
            
            CreateTable(
                "dbo.DWH_ONTIME_DN",
                c => new
                    {
                        DELVNO = c.String(nullable: false, maxLength: 10),
                        SALEORDNO = c.String(maxLength: 10),
                        SHPMNTNO = c.String(maxLength: 10),
                        WORK_TYPE = c.String(maxLength: 3),
                        ACTGIDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        SHCRDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        SHUPDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        TNRDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACPDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        FLEET_ID = c.String(maxLength: 5),
                        SEGMENT_ID = c.String(maxLength: 3),
                        MATFRIGRP = c.String(maxLength: 8),
                        SHPPOINT = c.String(maxLength: 4),
                        REGION_ID = c.String(maxLength: 4),
                        REGION_ID_SAP = c.String(maxLength: 4),
                        CARRIER_ID = c.String(maxLength: 10),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SHIPTO = c.String(maxLength: 10),
                        CRDLVDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLDLVDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACDLVDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PICKDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        DISTANCE = c.Double(),
                        INCOTERM = c.String(maxLength: 3),
                        ORDCMPDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        REQUESTED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ENDSHPDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        DOCRETDATE_SCGL = c.DateTime(precision: 7, storeType: "datetime2"),
                        DOCRETDATE_CUST = c.DateTime(precision: 7, storeType: "datetime2"),
                        INB_ONTIME_FLAG = c.Int(),
                        INB_COUNT = c.Int(),
                        INB_REASON_ID = c.Int(),
                        INB_REASON = c.String(),
                        INB_ADJUST = c.Int(),
                        INB_ADJUST_BY = c.String(maxLength: 100),
                        INB_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        OUTB_ONTIME_FLAG = c.Int(),
                        OUTB_COUNT = c.Int(),
                        OUTB_REASON_ID = c.Int(),
                        OUTB_REASON = c.String(),
                        OUTB_ADJUST = c.Int(),
                        OUTB_ADJUST_BY = c.String(maxLength: 100),
                        OUTB_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ON_TIME_FLAG = c.Int(),
                        ON_TIME_COUNT = c.Int(),
                        ON_TIME_REASON_ID = c.Int(),
                        ON_TIME_REASON = c.String(),
                        ON_TIME_ADJUST = c.Int(),
                        ON_TIME_ADJUST_BY = c.String(maxLength: 100),
                        ON_TIME_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        SCGL_DOCRET_ONTIME_FLAG = c.Int(),
                        SCGL_DOCRET_COUNT = c.Int(),
                        SCGL_DOCRET_REASON_ID = c.Int(),
                        SCGL_DOCRET_REASON = c.String(),
                        SCGL_DOCRET_ADJUST = c.Int(),
                        SCGL_DOCRET_ADJUST_BY = c.String(maxLength: 100),
                        SCGL_DOCRET_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        CUST_DOCRET_ONTIME_FLAG = c.Int(),
                        CUST_DOCRET_COUNT = c.Int(),
                        CUST_DOCRET_REASON_ID = c.Int(),
                        CUST_DOCRET_REASON = c.String(),
                        CUST_DOCRET_ADJUST = c.Int(),
                        CUST_DOCRET_ADJUST_BY = c.String(maxLength: 100),
                        CUST_DOCRET_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        TRUCK_TYPE = c.String(maxLength: 20),
                        PLNINBDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNOUTBDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNONTIMEDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNONTIMEDATE_WINDOW = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNDOCRETDATE_SCGL = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNDOCRETDATE_CUST = c.DateTime(precision: 7, storeType: "datetime2"),
                        INB_ORI_ONTIME = c.DateTime(precision: 7, storeType: "datetime2"),
                        INB_ORI_ONTIME_REASON_ID = c.Int(),
                        INB_ORI_ONTIME_REASON = c.String(),
                        INB_ORI_ONTIME_ADJUST = c.Int(),
                        INB_ORI_ONTIME_ADJUST_BY = c.String(maxLength: 100),
                        INB_ORI_ONTIME_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        INB_DES_ONTIME = c.DateTime(precision: 7, storeType: "datetime2"),
                        INB_DES_ONTIME_REASON_ID = c.Int(),
                        INB_DES_ONTIME_REASON = c.String(),
                        INB_DES_ONTIME_ADJUST = c.Int(),
                        INB_DES_ONTIME_ADJUST_BY = c.String(maxLength: 100),
                        INB_DES_ONTIME_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        OUTB_ORI_ONTIME = c.DateTime(precision: 7, storeType: "datetime2"),
                        OUTB_ORI_ONTIME_REASON_ID = c.Int(),
                        OUTB_ORI_ONTIME_REASON = c.String(),
                        OUTB_ORI_ONTIME_ADJUST = c.Int(),
                        OUTB_ORI_ONTIME_ADJUST_BY = c.String(maxLength: 100),
                        OUTB_ORI_ONTIME_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        OUTB_DES_ONTIME = c.DateTime(precision: 7, storeType: "datetime2"),
                        OUTB_DES_ONTIME_REASON_ID = c.Int(),
                        OUTB_DES_ONTIME_REASON = c.String(),
                        OUTB_DES_ONTIME_ADJUST = c.Int(),
                        OUTB_DES_ONTIME_ADJUST_BY = c.String(maxLength: 100),
                        OUTB_DES_ONTIME_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        DATA_GRP = c.String(maxLength: 20),
                        DATA_SUBGRP = c.String(maxLength: 20),
                        LOADEDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACTGIDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        TNRDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACPDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        CRDLVDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        REQUESTED_DATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        SHCRDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        SHUPDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLDLVDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACDLVDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PICKDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        ORDCMPDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        ENDSHPDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        DOCRETDATE_SCGL_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        DOCRETDATE_CUST_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNINBDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNOUTBDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNONTIMEDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNONTIMEDATE_WINDOW_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNDOCRETDATE_SCGL_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNDOCRETDATE_CUST_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        DEPARTMENT_ID = c.String(maxLength: 5),
                        DEPARTMENT_Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.DELVNO);
            
            CreateTable(
                "dbo.DWH_ONTIME_SHIPMENT",
                c => new
                    {
                        SHPMNTNO = c.String(nullable: false, maxLength: 10),
                        DELVNO = c.String(maxLength: 50),
                        WORK_TYPE = c.String(maxLength: 3),
                        ACTGIDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACTGIDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        FLEET_ID = c.String(maxLength: 5),
                        SEGMENT_ID = c.String(maxLength: 3),
                        MATFRIGRP = c.String(maxLength: 8),
                        CARRIER_ID = c.Int(),
                        REGION_ID = c.String(maxLength: 5),
                        REGION_ID_SAP = c.String(maxLength: 4),
                        SOLDTO = c.String(maxLength: 10),
                        SOLDTO_NAME = c.String(maxLength: 800),
                        SECTION_ID = c.String(maxLength: 5),
                        SECTION_NAME = c.String(maxLength: 100),
                        SHIPTO = c.String(maxLength: 10),
                        SHPPOINT = c.String(maxLength: 4),
                        INCOTERM = c.String(maxLength: 3),
                        SHCRDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        SHCRDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        FTNRDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        FTNRDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LTNRDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LTNRDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        LACPDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LACPDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        TNRD_ONTIME = c.Int(),
                        TNRD_COUNT = c.Int(),
                        TNRD_ONTIME_REASON_ID = c.Int(),
                        TNRD_ONTIME_REASON = c.String(),
                        TNRD_ADJUST = c.Int(),
                        TNRD_ADJUST_BY = c.String(maxLength: 100),
                        TNRD_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        ACPD_ONTIME = c.Int(),
                        ACPD_COUNT = c.Int(),
                        ACPD_REASON_ID = c.Int(),
                        ACPD_REASON = c.String(),
                        ACPD_ADJUST = c.Int(),
                        ACPD_ADJUST_BY = c.String(maxLength: 100),
                        ACPD_ADJUST_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNTNRDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNTNRDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNACPDDATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        PLNACPDDATE_D = c.DateTime(precision: 7, storeType: "datetime2"),
                        DATA_GRP = c.String(maxLength: 20),
                        DATA_SUBGRP = c.String(maxLength: 20),
                        LOADEDDATE = c.DateTime(),
                        DEPARTMENT_ID = c.String(maxLength: 5),
                        DEPARTMENT_Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SHPMNTNO);
            
            CreateTable(
                "dbo.KPIFrequencies",
                c => new
                    {
                        KPIFrequencyId = c.String(nullable: false, maxLength: 5),
                        KPIFrequencyName = c.String(),
                    })
                .PrimaryKey(t => t.KPIFrequencyId)
                .Index(t => t.KPIFrequencyId, unique: true);
            
            CreateTable(
                "dbo.MenuTables",
                c => new
                    {
                        DepartmentId = c.String(nullable: false, maxLength: 5),
                        SectionId = c.String(nullable: false, maxLength: 5),
                        KPIId = c.String(nullable: false, maxLength: 5),
                        KPIFrequencyId = c.String(nullable: false, maxLength: 5),
                        DepartmentName = c.String(),
                        SectionName = c.String(),
                        KpiName = c.String(),
                        KPIFrequencyName = c.String(),
                    })
                .PrimaryKey(t => new { t.DepartmentId, t.SectionId, t.KPIId, t.KPIFrequencyId })
                .ForeignKey("dbo.KPIs", t => t.KPIId, cascadeDelete: false)
                .ForeignKey("dbo.KPIFrequencies", t => t.KPIFrequencyId, cascadeDelete: false)
                .Index(t => t.KPIId)
                .Index(t => t.KPIFrequencyId);
            
            CreateTable(
                "dbo.KPIs",
                c => new
                    {
                        KpiId = c.String(nullable: false, maxLength: 5),
                        KpiName = c.String(),
                    })
                .PrimaryKey(t => t.KpiId)
                .Index(t => t.KpiId, unique: true);
            
            CreateTable(
                "dbo.OntimeAccepts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActualGiDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        MatName = c.String(),
                        MatFriGrp = c.String(),
                        SumOfAccept = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustAccept = c.Int(nullable: false),
                        SumOfAdjustAccept = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OntimeTenders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActualGiDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        MatName = c.String(),
                        MatFriGrp = c.String(),
                        SumOfTender = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustTender = c.Int(nullable: false),
                        SumOfAdjustTender = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 50),
                        RoleDesc = c.String(nullable: false, maxLength: 160),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.RoleId, unique: true)
                .Index(t => t.RoleDesc, unique: true);
            
            CreateTable(
                "dbo.TUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        RoleId = c.String(maxLength: 50),
                        LastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.UserEmail, unique: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TUsers", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MenuTables", "KPIFrequencyId", "dbo.KPIFrequencies");
            DropForeignKey("dbo.MenuTables", "KPIId", "dbo.KPIs");
            DropIndex("dbo.TUsers", new[] { "RoleId" });
            DropIndex("dbo.TUsers", new[] { "UserEmail" });
            DropIndex("dbo.Roles", new[] { "RoleDesc" });
            DropIndex("dbo.Roles", new[] { "RoleId" });
            DropIndex("dbo.KPIs", new[] { "KpiId" });
            DropIndex("dbo.MenuTables", new[] { "KPIFrequencyId" });
            DropIndex("dbo.MenuTables", new[] { "KPIId" });
            DropIndex("dbo.KPIFrequencies", new[] { "KPIFrequencyId" });
            DropTable("dbo.TUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.OntimeTenders");
            DropTable("dbo.OntimeAccepts");
            DropTable("dbo.KPIs");
            DropTable("dbo.MenuTables");
            DropTable("dbo.KPIFrequencies");
            DropTable("dbo.DWH_ONTIME_SHIPMENT");
            DropTable("dbo.DWH_ONTIME_DN");
            DropTable("dbo.DOM_SAP_MATFRIGRP");
            DropTable("dbo.DOM_MDS_ORGANIZATION");
            DropTable("dbo.DOM_CMD_VENDOR");
        }
    }
}
