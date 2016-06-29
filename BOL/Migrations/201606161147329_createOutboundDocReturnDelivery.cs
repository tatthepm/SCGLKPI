namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOutboundDocReturnDelivery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocReturnDelays",
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
                "dbo.OntimeDelays",
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
                "dbo.OntimeDeliveries",
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
                        SumOfDelivery = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustDelivery = c.Int(nullable: false),
                        SumOfAdjustDelivery = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OntimeDeliveryMonths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        Month = c.String(maxLength: 4),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        MatFriGrp = c.String(),
                        MatName = c.String(),
                        SumOfDelivery = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustDelivery = c.Int(nullable: false),
                        SumOfAdjustDelivery = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OntimeDeliveryYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        MatFriGrp = c.String(),
                        MatName = c.String(),
                        SumOfDelivery = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustDelivery = c.Int(nullable: false),
                        SumOfAdjustDelivery = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OntimeDocReturnMonths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        Month = c.String(maxLength: 4),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        MatFriGrp = c.String(),
                        MatName = c.String(),
                        SumOfDocReturn = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustDocRetunr = c.Int(nullable: false),
                        SumOfAdjustDocReturn = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OntimeDocReturns",
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
                        SumOfDocReturn = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustDocReturn = c.Int(nullable: false),
                        SumOfAdjustDocReturn = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OntimeDocReturnYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        MatFriGrp = c.String(),
                        MatName = c.String(),
                        SumOfDocReturn = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustDocReturn = c.Int(nullable: false),
                        SumOfAdjustDocReturn = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReasonDocReturns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ReasonOntimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReasonOntimes", new[] { "Name" });
            DropIndex("dbo.ReasonDocReturns", new[] { "Name" });
            DropIndex("dbo.OntimeDelays", new[] { "ACDLVDATE_D" });
            DropIndex("dbo.OntimeDelays", new[] { "SECTION_ID" });
            DropIndex("dbo.OntimeDelays", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.OntimeDelays", new[] { "MATFRIGRP" });
            DropIndex("dbo.DocReturnDelays", new[] { "DOCRETDATE_SCGL_D" });
            DropIndex("dbo.DocReturnDelays", new[] { "SECTION_ID" });
            DropIndex("dbo.DocReturnDelays", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.DocReturnDelays", new[] { "MATFRIGRP" });
            DropTable("dbo.ReasonOntimes");
            DropTable("dbo.ReasonDocReturns");
            DropTable("dbo.OntimeDocReturnYears");
            DropTable("dbo.OntimeDocReturns");
            DropTable("dbo.OntimeDocReturnMonths");
            DropTable("dbo.OntimeDeliveryYears");
            DropTable("dbo.OntimeDeliveryMonths");
            DropTable("dbo.OntimeDeliveries");
            DropTable("dbo.OntimeDelays");
            DropTable("dbo.DocReturnDelays");
        }
    }
}
