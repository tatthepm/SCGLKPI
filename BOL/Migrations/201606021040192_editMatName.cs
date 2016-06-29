namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMatName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "MATNAME", c => c.String());
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "MATNAM");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "MATNAM", c => c.String());
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "MATNAME");
        }
    }
}
