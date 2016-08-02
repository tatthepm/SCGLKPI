namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homekpi1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HomeKPIs");
            AddColumn("dbo.HomeKPIs", "DepartmentId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.HomeKPIs", "SectionId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.HomeKPIs", "Segment", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.HomeKPIs", "DepartmentName", c => c.String());
            AddColumn("dbo.HomeKPIs", "SectionName", c => c.String());
            AddPrimaryKey("dbo.HomeKPIs", new[] { "month_year", "DepartmentId", "SectionId", "Segment" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.HomeKPIs");
            DropColumn("dbo.HomeKPIs", "SectionName");
            DropColumn("dbo.HomeKPIs", "DepartmentName");
            DropColumn("dbo.HomeKPIs", "Segment");
            DropColumn("dbo.HomeKPIs", "SectionId");
            DropColumn("dbo.HomeKPIs", "DepartmentId");
            AddPrimaryKey("dbo.HomeKPIs", "month_year");
        }
    }
}
