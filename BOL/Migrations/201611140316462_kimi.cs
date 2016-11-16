namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kimi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcceptPendings", "DATEDIFF", c => c.Int(nullable: false));
            AddColumn("dbo.DocReturnPendings", "DATEDIFF", c => c.Int(nullable: false));
            AddColumn("dbo.InboundPendings", "DATEDIFF", c => c.Int(nullable: false));
            AddColumn("dbo.OntimePendings", "DATEDIFF", c => c.Int(nullable: false));
            AddColumn("dbo.OutboundPendings", "DATEDIFF", c => c.Int(nullable: false));
            AddColumn("dbo.TenderPendings", "DATEDIFF", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderPendings", "DATEDIFF");
            DropColumn("dbo.OutboundPendings", "DATEDIFF");
            DropColumn("dbo.OntimePendings", "DATEDIFF");
            DropColumn("dbo.InboundPendings", "DATEDIFF");
            DropColumn("dbo.DocReturnPendings", "DATEDIFF");
            DropColumn("dbo.AcceptPendings", "DATEDIFF");
        }
    }
}
