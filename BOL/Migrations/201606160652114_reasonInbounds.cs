namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reasonInbounds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReasonInbounds",
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
            DropIndex("dbo.ReasonInbounds", new[] { "Name" });
            DropTable("dbo.ReasonInbounds");
        }
    }
}
