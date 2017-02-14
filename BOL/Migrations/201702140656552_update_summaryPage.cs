namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_summaryPage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarrierSummaryDailies", "CarrierId", c => c.String(maxLength: 10));
            AlterColumn("dbo.SaleSummaryDailies", "CustomerId", c => c.String(maxLength: 10));
            AlterColumn("dbo.SaleSummaryDailies", "SegmentId", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SaleSummaryDailies", "SegmentId", c => c.String(maxLength: 5));
            AlterColumn("dbo.SaleSummaryDailies", "CustomerId", c => c.String(maxLength: 5));
            AlterColumn("dbo.CarrierSummaryDailies", "CarrierId", c => c.String(maxLength: 5));
        }
    }
}
