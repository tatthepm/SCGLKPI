namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedbfix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Carriers", new[] { "Year" });
            DropIndex("dbo.MatFreightGroups", new[] { "Year" });
            DropIndex("dbo.ShippingPoints", new[] { "Year" });
            DropIndex("dbo.TenderUsers", new[] { "Year" });
            DropTable("dbo.Carriers");
            DropTable("dbo.MatFreightGroups");
            DropTable("dbo.ShippingPoints");
            DropTable("dbo.TenderUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TenderUsers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 40),
                        Month = c.String(),
                        UserName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ShippingPoints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 40),
                        Month = c.String(),
                        ShippingPointsId = c.String(),
                        ShippingPointsName = c.String(),
                        ShipToId = c.String(),
                        ShipToName = c.String(),
                        SoldToId = c.String(),
                        SoldToName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.MatFreightGroups",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 40),
                        Month = c.String(),
                        MatFriGrp = c.String(),
                        MatName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 40),
                        Month = c.String(),
                        CarrierId = c.String(),
                        CarrierName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.TenderUsers", "Year", unique: true);
            CreateIndex("dbo.ShippingPoints", "Year", unique: true);
            CreateIndex("dbo.MatFreightGroups", "Year", unique: true);
            CreateIndex("dbo.Carriers", "Year", unique: true);
        }
    }
}
