namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shtrucktype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "TRUCK_TYPE", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "TRUCK_TYPE");
        }
    }
}
