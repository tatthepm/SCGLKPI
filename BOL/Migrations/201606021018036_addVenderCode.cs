namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVenderCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcceptedDelays", "VENDOR_CODE", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcceptedDelays", "VENDOR_CODE");
        }
    }
}
