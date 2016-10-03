namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TenderedFiles");
            DropColumn("dbo.TenderedFiles", "ID");
            AddColumn("dbo.TenderedFiles", "FileId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TenderedFiles", "FileId");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.TenderedFiles", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TenderedFiles");
            DropColumn("dbo.TenderedFiles", "FileId");
            AddPrimaryKey("dbo.TenderedFiles", "ID");
        }
    }
}
