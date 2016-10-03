namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TenderedFiles");
            DropColumn("dbo.TenderedFiles", "FileId");
            AddColumn("dbo.TenderedFiles", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TenderedFiles", "ID");

        }
        
        public override void Down()
        {
            AddColumn("dbo.TenderedFiles", "FileId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TenderedFiles");
            DropColumn("dbo.TenderedFiles", "ID");
            AddPrimaryKey("dbo.TenderedFiles", "FileId");
        }
    }
}
