namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createReasonTendered : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReasonTendereds",
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
            DropIndex("dbo.ReasonTendereds", new[] { "Name" });
            DropTable("dbo.ReasonTendereds");
        }
    }
}
