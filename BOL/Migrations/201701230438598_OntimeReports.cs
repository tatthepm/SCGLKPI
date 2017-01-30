namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OntimeReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OntimeReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActualGiDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DepartmentId = c.String(maxLength: 5),
                        DepartmentName = c.String(),
                        SectionId = c.String(maxLength: 5),
                        SectionName = c.String(),
                        Segment = c.String(maxLength: 20),
                        SubSegment = c.String(maxLength: 20),
                        MatName = c.String(),
                        MatFriGrp = c.String(),
                        TruckType = c.String(maxLength: 20),
                        SoldToId = c.String(maxLength: 10),
                        SoldToName = c.String(maxLength: 800),
                        CarrierId = c.String(maxLength: 10),
                        CarrierName = c.String(maxLength: 800),
                        SumOfTender = c.Int(nullable: false),
                        OnTimeTender = c.Double(nullable: false),
                        AdjustTender = c.Double(nullable: false),
                        SumOfAccept = c.Int(nullable: false),
                        OnTimeAccept = c.Double(nullable: false),
                        AdjustAccept = c.Double(nullable: false),
                        SumOfInbound = c.Int(nullable: false),
                        OnTimeInbound = c.Double(nullable: false),
                        AdjustInbound = c.Double(nullable: false),
                        SumOfOutbound = c.Int(nullable: false),
                        OnTimeOutbound = c.Double(nullable: false),
                        AdjustOutbound = c.Double(nullable: false),
                        SumOfOntime = c.Int(nullable: false),
                        OnTimeOntime = c.Double(nullable: false),
                        AdjustOntime = c.Double(nullable: false),
                        SumOfDocReturn = c.Int(nullable: false),
                        OnTimeDocReturn = c.Double(nullable: false),
                        AdjustDocReturn = c.Double(nullable: false),
                        Plan = c.Double(nullable: false),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OntimeReports");
        }
    }
}
