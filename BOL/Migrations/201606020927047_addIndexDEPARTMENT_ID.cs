namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIndexDEPARTMENT_ID : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DWH_ONTIME_SHIPMENT", "SECTION_ID");
            CreateIndex("dbo.DWH_ONTIME_SHIPMENT", "DEPARTMENT_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DWH_ONTIME_SHIPMENT", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.DWH_ONTIME_SHIPMENT", new[] { "SECTION_ID" });
        }
    }
}
