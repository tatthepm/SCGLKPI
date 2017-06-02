namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OntimeTenderMonths", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.OntimeTenderMonths", "UPDT_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.OntimeTenders", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.OntimeTenders", "UPDT_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.OntimeTenderYears", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.OntimeTenderYears", "UPDT_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.TenderedAdjusteds", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.TenderedAdjusteds", "UPDT_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.TenderedDelays", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.TenderedDelays", "UPDT_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.TenderPendings", "CRTD_USR_CD", c => c.String(maxLength: 40));
            AddColumn("dbo.TenderPendings", "UPDT_USR_CD", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderPendings", "UPDT_USR_CD");
            DropColumn("dbo.TenderPendings", "CRTD_USR_CD");
            DropColumn("dbo.TenderedDelays", "UPDT_USR_CD");
            DropColumn("dbo.TenderedDelays", "CRTD_USR_CD");
            DropColumn("dbo.TenderedAdjusteds", "UPDT_USR_CD");
            DropColumn("dbo.TenderedAdjusteds", "CRTD_USR_CD");
            DropColumn("dbo.OntimeTenderYears", "UPDT_USR_CD");
            DropColumn("dbo.OntimeTenderYears", "CRTD_USR_CD");
            DropColumn("dbo.OntimeTenders", "UPDT_USR_CD");
            DropColumn("dbo.OntimeTenders", "CRTD_USR_CD");
            DropColumn("dbo.OntimeTenderMonths", "UPDT_USR_CD");
            DropColumn("dbo.OntimeTenderMonths", "CRTD_USR_CD");
        }
    }
}
