namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKPITender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OntimeTenderMonths", "SHIPTO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimeTenderMonths", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OntimeTenderMonths", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OntimeTenders", "SHIPTO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimeTenders", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OntimeTenders", "TRUCK_TYPE", c => c.String(maxLength: 20));
            AddColumn("dbo.OntimeTenderYears", "SHIPTO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimeTenderYears", "SHPPOINT", c => c.String(maxLength: 4));
            AddColumn("dbo.OntimeTenderYears", "TRUCK_TYPE", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OntimeTenderYears", "TRUCK_TYPE");
            DropColumn("dbo.OntimeTenderYears", "SHPPOINT");
            DropColumn("dbo.OntimeTenderYears", "SHIPTO");
            DropColumn("dbo.OntimeTenders", "TRUCK_TYPE");
            DropColumn("dbo.OntimeTenders", "SHPPOINT");
            DropColumn("dbo.OntimeTenders", "SHIPTO");
            DropColumn("dbo.OntimeTenderMonths", "TRUCK_TYPE");
            DropColumn("dbo.OntimeTenderMonths", "SHPPOINT");
            DropColumn("dbo.OntimeTenderMonths", "SHIPTO");
        }
    }
}
