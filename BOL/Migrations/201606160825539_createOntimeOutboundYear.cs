namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOntimeOutboundYear : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OntimeOutboundYears",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OntimeOutboundYears");
        }
    }
}
