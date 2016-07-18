namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tenderindex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TenderedAdjusteds", new[] { "MATFRIGRP" });
            DropIndex("dbo.TenderedAdjusteds", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.TenderedAdjusteds", new[] { "SECTION_ID" });
            DropIndex("dbo.TenderedDelays", new[] { "MATFRIGRP" });
            DropIndex("dbo.TenderedDelays", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.TenderedDelays", new[] { "SECTION_ID" });
            DropIndex("dbo.TenderPendings", new[] { "MATFRIGRP" });
            DropIndex("dbo.TenderPendings", new[] { "DEPARTMENT_ID" });
            DropIndex("dbo.TenderPendings", new[] { "SECTION_ID" });
            CreateIndex("dbo.TenderedAdjusteds", "SEGMENT");
            CreateIndex("dbo.TenderedDelays", "SEGMENT");
            CreateIndex("dbo.TenderPendings", "SEGMENT");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TenderPendings", new[] { "SEGMENT" });
            DropIndex("dbo.TenderedDelays", new[] { "SEGMENT" });
            DropIndex("dbo.TenderedAdjusteds", new[] { "SEGMENT" });
            CreateIndex("dbo.TenderPendings", "SECTION_ID");
            CreateIndex("dbo.TenderPendings", "DEPARTMENT_ID");
            CreateIndex("dbo.TenderPendings", "MATFRIGRP");
            CreateIndex("dbo.TenderedDelays", "SECTION_ID");
            CreateIndex("dbo.TenderedDelays", "DEPARTMENT_ID");
            CreateIndex("dbo.TenderedDelays", "MATFRIGRP");
            CreateIndex("dbo.TenderedAdjusteds", "SECTION_ID");
            CreateIndex("dbo.TenderedAdjusteds", "DEPARTMENT_ID");
            CreateIndex("dbo.TenderedAdjusteds", "MATFRIGRP");
        }
    }
}
