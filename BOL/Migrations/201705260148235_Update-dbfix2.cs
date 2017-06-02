namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedbfix2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        Month = c.String(maxLength: 2),
                        CarrierId = c.String(maxLength: 15),
                        CarrierName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.CarrierId);
            
            CreateTable(
                "dbo.MatFreightGroups",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        Month = c.String(maxLength: 2),
                        MatFriGrp = c.String(maxLength: 10),
                        MatName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.MatFriGrp);
            
            CreateTable(
                "dbo.ShippingPoints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        Month = c.String(maxLength: 2),
                        ShippingPointsId = c.String(maxLength: 15),
                        ShippingPointsName = c.String(),
                        ShipToId = c.String(maxLength: 15),
                        ShipToName = c.String(),
                        SoldToId = c.String(maxLength: 15),
                        SoldToName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.ShippingPointsId)
                .Index(t => t.ShipToId)
                .Index(t => t.SoldToId);
            
            CreateTable(
                "dbo.TenderUsers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        Month = c.String(maxLength: 2),
                        UserName = c.String(maxLength: 40),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TenderUsers", new[] { "UserName" });
            DropIndex("dbo.ShippingPoints", new[] { "SoldToId" });
            DropIndex("dbo.ShippingPoints", new[] { "ShipToId" });
            DropIndex("dbo.ShippingPoints", new[] { "ShippingPointsId" });
            DropIndex("dbo.MatFreightGroups", new[] { "MatFriGrp" });
            DropIndex("dbo.Carriers", new[] { "CarrierId" });
            DropTable("dbo.TenderUsers");
            DropTable("dbo.ShippingPoints");
            DropTable("dbo.MatFreightGroups");
            DropTable("dbo.Carriers");
        }
    }
}
