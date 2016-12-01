namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanDocRet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocReturnPendings", "TRACKING", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocReturnPendings", "TRACKING");
        }
    }
}
