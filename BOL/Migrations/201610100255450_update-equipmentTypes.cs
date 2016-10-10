namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateequipmentTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Eqmt_Code = c.String(maxLength: 4),
                        Eqmt_Description = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.Eqmt_Code, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EquipmentTypes", new[] { "Eqmt_Code" });
            DropTable("dbo.EquipmentTypes");
        }
    }
}
