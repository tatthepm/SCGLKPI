namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedbShippoint : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DOM_SAP_SHPPOINT",
                c => new
                    {
                        SHIPPOINT = c.String(nullable: false, maxLength: 4),
                        DESP = c.String(maxLength: 30),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SHIPPOINT);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DOM_SAP_SHPPOINT");
        }
    }
}
