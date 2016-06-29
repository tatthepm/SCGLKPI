namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRemark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "TNRD_ONTIME_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "ACPD_REMARK", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "ACPD_REMARK");
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "TNRD_ONTIME_REMARK");
        }
    }
}
