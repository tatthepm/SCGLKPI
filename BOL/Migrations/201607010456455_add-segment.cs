namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsegment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OntimeTenderMonths", "Segment", c => c.String(maxLength: 20));
            AddColumn("dbo.OntimeTenders", "Segment", c => c.String(maxLength: 20));
            AddColumn("dbo.OntimeTenderYears", "Segment", c => c.String(maxLength: 20));
            AddColumn("dbo.TenderedDelays", "SEGMENT", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderedDelays", "SEGMENT");
            DropColumn("dbo.OntimeTenderYears", "Segment");
            DropColumn("dbo.OntimeTenders", "Segment");
            DropColumn("dbo.OntimeTenderMonths", "Segment");
        }
    }
}
