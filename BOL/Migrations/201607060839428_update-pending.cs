namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepending : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.DWH_ONTIME_SHIPMENT", name: "IX_SECTION_ID", newName: "DN_SECTION");
            RenameIndex(table: "dbo.DWH_ONTIME_SHIPMENT", name: "IX_LACPDDATE_D", newName: "DN_LACPDDATE");
            RenameIndex(table: "dbo.DWH_ONTIME_SHIPMENT", name: "IX_DEPARTMENT_ID", newName: "DN_DEPARTMENT");
            CreateTable(
                "dbo.HomeKPIs",
                c => new
                    {
                        month_year = c.String(nullable: false, maxLength: 128),
                        daysDIff = c.String(maxLength: 10),
                        LastMonth = c.String(maxLength: 10),
                        Year = c.String(maxLength: 4),
                        shipmentLastMonthCount = c.String(maxLength: 15),
                        DNLastMonthCount = c.String(maxLength: 15),
                        PercentOntime = c.String(maxLength: 15),
                        PercentDelay = c.String(maxLength: 15),
                        PercentPending = c.String(maxLength: 15),
                        OntimeCount = c.String(maxLength: 15),
                        DelayCount = c.String(maxLength: 15),
                        PendingCount = c.String(maxLength: 15),
                        TenderLastMonthCount = c.String(maxLength: 15),
                        AcceptLastMonthCount = c.String(maxLength: 15),
                        InboundLastMonthCount = c.String(maxLength: 15),
                        OutboundLastMonthCount = c.String(maxLength: 15),
                        DeliveryLastMonthCount = c.String(maxLength: 15),
                        DocReturnLastMonthCount = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.month_year);
            
            CreateIndex("dbo.DWH_ONTIME_DN", "ACTGIDATE");
            CreateIndex("dbo.DWH_ONTIME_DN", "ACTGIDATE_D");
            CreateIndex("dbo.DWH_ONTIME_DN", "ACDLVDATE_D");
            CreateIndex("dbo.DWH_ONTIME_DN", "DOCRETDATE_SCGL_D");
            CreateIndex("dbo.DWH_ONTIME_SHIPMENT", "SHCRDATE_D", name: "DN_SHCRDATE");
            CreateIndex("dbo.DWH_ONTIME_SHIPMENT", "FTNRDDATE_D", name: "DN_FTNRDDATE");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DWH_ONTIME_SHIPMENT", "DN_FTNRDDATE");
            DropIndex("dbo.DWH_ONTIME_SHIPMENT", "DN_SHCRDATE");
            DropIndex("dbo.DWH_ONTIME_DN", new[] { "DOCRETDATE_SCGL_D" });
            DropIndex("dbo.DWH_ONTIME_DN", new[] { "ACDLVDATE_D" });
            DropIndex("dbo.DWH_ONTIME_DN", new[] { "ACTGIDATE_D" });
            DropIndex("dbo.DWH_ONTIME_DN", new[] { "ACTGIDATE" });
            DropTable("dbo.HomeKPIs");
            RenameIndex(table: "dbo.DWH_ONTIME_SHIPMENT", name: "DN_DEPARTMENT", newName: "IX_DEPARTMENT_ID");
            RenameIndex(table: "dbo.DWH_ONTIME_SHIPMENT", name: "DN_LACPDDATE", newName: "IX_LACPDDATE_D");
            RenameIndex(table: "dbo.DWH_ONTIME_SHIPMENT", name: "DN_SECTION", newName: "IX_SECTION_ID");
        }
    }
}
