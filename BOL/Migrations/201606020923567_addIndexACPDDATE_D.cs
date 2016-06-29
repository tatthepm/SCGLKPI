namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIndexACPDDATE_D : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DWH_ONTIME_SHIPMENT", "LACPDDATE_D");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DWH_ONTIME_SHIPMENT", new[] { "LACPDDATE_D" });
        }
    }
}
