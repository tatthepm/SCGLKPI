namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOntimeOutbound : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OntimeOutbounds",
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
                        SumOfOutbound = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustOutbound = c.Int(nullable: false),
                        SumOfAdjustOutbound = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.DWH_ONTIME_DN", "CARRIER_ID", c => c.String(maxLength: 32));
            AlterColumn("dbo.DWH_ONTIME_SHIPMENT", "CARRIER_ID", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DWH_ONTIME_SHIPMENT", "CARRIER_ID", c => c.Int());
            AlterColumn("dbo.DWH_ONTIME_DN", "CARRIER_ID", c => c.String(maxLength: 10));
            DropTable("dbo.OntimeOutbounds");
        }
    }
}
