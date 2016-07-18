namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenewMethod : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AcceptPendings", "PLNACPDDATE_D");
            CreateIndex("dbo.DocReturnPendings", "PLNDOCRETDATE_SCGL_D");
            CreateIndex("dbo.InboundPendings", "PLNINBDATE_D");
            CreateIndex("dbo.OntimePendings", "PLNONTIMEDATE_D");
            CreateIndex("dbo.OutboundPendings", "PLNOUTBDATE_D");
            CreateIndex("dbo.TenderPendings", "PLNTNRDDATE_D");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TenderPendings", new[] { "PLNTNRDDATE_D" });
            DropIndex("dbo.OutboundPendings", new[] { "PLNOUTBDATE_D" });
            DropIndex("dbo.OntimePendings", new[] { "PLNONTIMEDATE_D" });
            DropIndex("dbo.InboundPendings", new[] { "PLNINBDATE_D" });
            DropIndex("dbo.DocReturnPendings", new[] { "PLNDOCRETDATE_SCGL_D" });
            DropIndex("dbo.AcceptPendings", new[] { "PLNACPDDATE_D" });
        }
    }
}
