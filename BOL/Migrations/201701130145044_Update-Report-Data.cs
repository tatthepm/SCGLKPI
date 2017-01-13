namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReportData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OntimeAccepts", "ActualGiDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeTenders", "ActualGiDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.OntimeAccepts", "AcceptDate");
            DropColumn("dbo.OntimeTenders", "FirstTenderDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OntimeTenders", "FirstTenderDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAccepts", "AcceptDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.OntimeTenders", "ActualGiDate");
            DropColumn("dbo.OntimeAccepts", "ActualGiDate");
        }
    }
}
