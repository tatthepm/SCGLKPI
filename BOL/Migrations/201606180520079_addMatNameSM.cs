namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMatNameSM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DWH_ONTIME_DN", "MATNAME", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DWH_ONTIME_DN", "MATNAME");
        }
    }
}
