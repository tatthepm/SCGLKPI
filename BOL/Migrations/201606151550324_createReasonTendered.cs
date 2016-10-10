namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createEquipmentTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentTypess",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EquipmentTypess", new[] { "Name" });
            DropTable("dbo.EquipmentTypess");
        }
    }
}
