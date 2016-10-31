namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddmissingDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TenderPendings", "DELVNO", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderPendings", "DELVNO");
        }
    }
}
