namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubSegment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomeKPIs", "SubSegment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomeKPIs", "SubSegment");
        }
    }
}
