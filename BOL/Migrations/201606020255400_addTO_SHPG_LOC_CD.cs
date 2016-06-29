namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTO_SHPG_LOC_CD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DWH_ONTIME_DN", "INB_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "OUTB_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "ON_TIME_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "SCGL_DOCRET_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "CUST_DOCRET_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "INB_ORI_ONTIME_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "INB_DES_ONTIME_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "OUTB_ORI_ONTIME_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "OUTB_DES_ONTIME_REMARK", c => c.String());
            AddColumn("dbo.DWH_ONTIME_DN", "TO_SHPG_LOC_CD", c => c.String(maxLength: 64));
            AddColumn("dbo.DWH_ONTIME_DN", "TO_SHPG_LOC_NAME", c => c.String(maxLength: 280));
            AddColumn("dbo.DWH_ONTIME_DN", "REGION_NAME_TH", c => c.String(maxLength: 30));
            AddColumn("dbo.DWH_ONTIME_DN", "REGION_NAME_EN", c => c.String(maxLength: 30));
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "LAST_SHPG_LOC_CD", c => c.String(maxLength: 64));
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "LAST_SHPG_LOC_NAME", c => c.String(maxLength: 280));
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "REGION_NAME_TH", c => c.String(maxLength: 30));
            AddColumn("dbo.DWH_ONTIME_SHIPMENT", "REGION_NAME_EN", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "REGION_NAME_EN");
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "REGION_NAME_TH");
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "LAST_SHPG_LOC_NAME");
            DropColumn("dbo.DWH_ONTIME_SHIPMENT", "LAST_SHPG_LOC_CD");
            DropColumn("dbo.DWH_ONTIME_DN", "REGION_NAME_EN");
            DropColumn("dbo.DWH_ONTIME_DN", "REGION_NAME_TH");
            DropColumn("dbo.DWH_ONTIME_DN", "TO_SHPG_LOC_NAME");
            DropColumn("dbo.DWH_ONTIME_DN", "TO_SHPG_LOC_CD");
            DropColumn("dbo.DWH_ONTIME_DN", "OUTB_DES_ONTIME_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "OUTB_ORI_ONTIME_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "INB_DES_ONTIME_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "INB_ORI_ONTIME_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "CUST_DOCRET_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "SCGL_DOCRET_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "ON_TIME_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "OUTB_REMARK");
            DropColumn("dbo.DWH_ONTIME_DN", "INB_REMARK");
        }
    }
}
