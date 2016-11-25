namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHubMasters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HubMasters",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Hub_Id = c.String(maxLength: 13),
                        Hub_Name = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.Hub_Id, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.HubMasters", new[] { "Hub_Id" });
            DropTable("dbo.HubMasters");
        }
    }
}
