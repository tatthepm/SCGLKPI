namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusertender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DWH_ONTIME_DN", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.DWH_ONTIME_DN", "UPDT_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "UPDT_USR_CD", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "UPDT_USR_CD");
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "CRTD_USR_CD");
            DropColumn("dbo.DWH_ONTIME_DN", "UPDT_USR_CD");
            DropColumn("dbo.DWH_ONTIME_DN", "CRTD_USR_CD");
        }
    }
}
