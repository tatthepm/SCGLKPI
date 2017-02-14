namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatereport2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarrierSummaryDailies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActualGiDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LoadedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CarrierId = c.String(maxLength: 5),
                        CarrierName = c.String(),
                        RegionId = c.String(maxLength: 5),
                        RegionName = c.String(),
                        MatName = c.String(),
                        MatFriGrp = c.String(),
                        SumOfTender = c.Int(nullable: false),
                        OnTimeTender = c.Int(nullable: false),
                        AdjustedTender = c.Int(nullable: false),
                        SumOfAccept = c.Int(nullable: false),
                        OnTimeAccept = c.Int(nullable: false),
                        AdjustedAccept = c.Int(nullable: false),
                        SumOfInbound = c.Int(nullable: false),
                        OnTimeInbound = c.Int(nullable: false),
                        AdjustedInbound = c.Int(nullable: false),
                        SumOfOutbound = c.Int(nullable: false),
                        OnTimeOutbound = c.Int(nullable: false),
                        AdjustedOutbound = c.Int(nullable: false),
                        SumOfDelivery = c.Int(nullable: false),
                        OnTimeDelivery = c.Int(nullable: false),
                        AdjustedDelivery = c.Int(nullable: false),
                        SumOfDocreturn = c.Int(nullable: false),
                        OnTimeDocreturn = c.Int(nullable: false),
                        AdjustedDocreturn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ActualGiDate)
                .Index(t => t.LoadedDate);
            
            CreateTable(
                "dbo.OperationSummaryDailies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActualGiDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LoadedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        MatName = c.String(),
                        MatFriGrp = c.String(),
                        SumOfTender = c.Int(nullable: false),
                        OnTimeTender = c.Int(nullable: false),
                        AdjustedTender = c.Int(nullable: false),
                        SumOfAccept = c.Int(nullable: false),
                        OnTimeAccept = c.Int(nullable: false),
                        AdjustedAccept = c.Int(nullable: false),
                        SumOfInbound = c.Int(nullable: false),
                        OnTimeInbound = c.Int(nullable: false),
                        AdjustedInbound = c.Int(nullable: false),
                        SumOfOutbound = c.Int(nullable: false),
                        OnTimeOutbound = c.Int(nullable: false),
                        AdjustedOutbound = c.Int(nullable: false),
                        SumOfDelivery = c.Int(nullable: false),
                        OnTimeDelivery = c.Int(nullable: false),
                        AdjustedDelivery = c.Int(nullable: false),
                        SumOfDocreturn = c.Int(nullable: false),
                        OnTimeDocreturn = c.Int(nullable: false),
                        AdjustedDocreturn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ActualGiDate)
                .Index(t => t.LoadedDate);
            
            CreateTable(
                "dbo.SaleSummaryDailies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActualGiDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LoadedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CustomerId = c.String(maxLength: 5),
                        CustomerName = c.String(),
                        SegmentId = c.String(maxLength: 5),
                        SegmentName = c.String(),
                        MatName = c.String(),
                        MatFriGrp = c.String(),
                        SumOfTender = c.Int(nullable: false),
                        OnTimeTender = c.Int(nullable: false),
                        AdjustedTender = c.Int(nullable: false),
                        SumOfAccept = c.Int(nullable: false),
                        OnTimeAccept = c.Int(nullable: false),
                        AdjustedAccept = c.Int(nullable: false),
                        SumOfInbound = c.Int(nullable: false),
                        OnTimeInbound = c.Int(nullable: false),
                        AdjustedInbound = c.Int(nullable: false),
                        SumOfOutbound = c.Int(nullable: false),
                        OnTimeOutbound = c.Int(nullable: false),
                        AdjustedOutbound = c.Int(nullable: false),
                        SumOfDelivery = c.Int(nullable: false),
                        OnTimeDelivery = c.Int(nullable: false),
                        AdjustedDelivery = c.Int(nullable: false),
                        SumOfDocreturn = c.Int(nullable: false),
                        OnTimeDocreturn = c.Int(nullable: false),
                        AdjustedDocreturn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ActualGiDate)
                .Index(t => t.LoadedDate);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SaleSummaryDailies", new[] { "LoadedDate" });
            DropIndex("dbo.SaleSummaryDailies", new[] { "ActualGiDate" });
            DropIndex("dbo.OperationSummaryDailies", new[] { "LoadedDate" });
            DropIndex("dbo.OperationSummaryDailies", new[] { "ActualGiDate" });
            DropIndex("dbo.CarrierSummaryDailies", new[] { "LoadedDate" });
            DropIndex("dbo.CarrierSummaryDailies", new[] { "ActualGiDate" });
            DropTable("dbo.SaleSummaryDailies");
            DropTable("dbo.OperationSummaryDailies");
            DropTable("dbo.CarrierSummaryDailies");
        }
    }
}
