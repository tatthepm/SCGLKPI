namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createInbouds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OntimeInbounds",
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
                        SumOfInbound = c.Int(nullable: false),
                        OnTime = c.Int(nullable: false),
                        Delay = c.Int(nullable: false),
                        AdjustInbound = c.Int(nullable: false),
                        SumOfAdjustInbound = c.Int(nullable: false),
                        KpiId = c.String(maxLength: 5),
                        KpiName = c.String(),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OntimeInbounds");
        }
    }
}
