namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSHPMNTNO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcceptedAdjusteds", "DELVNO", c => c.String(maxLength: 10));
            AddColumn("dbo.AcceptedDelays", "DELVNO", c => c.String(maxLength: 10));
            AddColumn("dbo.AcceptPendings", "DELVNO", c => c.String(maxLength: 10));
            AddColumn("dbo.DocReturnAdjusteds", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.DocReturnAdjusteds", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.DocReturnDelays", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.DocReturnDelays", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.DocReturnPendings", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.DocReturnPendings", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.InboundAdjusteds", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.InboundAdjusteds", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.InboundDelays", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.InboundDelays", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.InboundPendings", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.InboundPendings", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimeAdjusteds", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimeAdjusteds", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimeDelays", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimeDelays", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimePendings", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OntimePendings", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OutboundAdjusteds", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OutboundAdjusteds", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OutboundDelays", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OutboundDelays", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OutboundPendings", "SALEORDNO", c => c.String(maxLength: 10));
            AddColumn("dbo.OutboundPendings", "SHPMNTNO", c => c.String(maxLength: 10));
            AddColumn("dbo.TenderedAdjusteds", "DELVNO", c => c.String(maxLength: 10));
            AddColumn("dbo.TenderedDelays", "DELVNO", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderedDelays", "DELVNO");
            DropColumn("dbo.TenderedAdjusteds", "DELVNO");
            DropColumn("dbo.OutboundPendings", "SHPMNTNO");
            DropColumn("dbo.OutboundPendings", "SALEORDNO");
            DropColumn("dbo.OutboundDelays", "SHPMNTNO");
            DropColumn("dbo.OutboundDelays", "SALEORDNO");
            DropColumn("dbo.OutboundAdjusteds", "SHPMNTNO");
            DropColumn("dbo.OutboundAdjusteds", "SALEORDNO");
            DropColumn("dbo.OntimePendings", "SHPMNTNO");
            DropColumn("dbo.OntimePendings", "SALEORDNO");
            DropColumn("dbo.OntimeDelays", "SHPMNTNO");
            DropColumn("dbo.OntimeDelays", "SALEORDNO");
            DropColumn("dbo.OntimeAdjusteds", "SHPMNTNO");
            DropColumn("dbo.OntimeAdjusteds", "SALEORDNO");
            DropColumn("dbo.InboundPendings", "SHPMNTNO");
            DropColumn("dbo.InboundPendings", "SALEORDNO");
            DropColumn("dbo.InboundDelays", "SHPMNTNO");
            DropColumn("dbo.InboundDelays", "SALEORDNO");
            DropColumn("dbo.InboundAdjusteds", "SHPMNTNO");
            DropColumn("dbo.InboundAdjusteds", "SALEORDNO");
            DropColumn("dbo.DocReturnPendings", "SHPMNTNO");
            DropColumn("dbo.DocReturnPendings", "SALEORDNO");
            DropColumn("dbo.DocReturnDelays", "SHPMNTNO");
            DropColumn("dbo.DocReturnDelays", "SALEORDNO");
            DropColumn("dbo.DocReturnAdjusteds", "SHPMNTNO");
            DropColumn("dbo.DocReturnAdjusteds", "SALEORDNO");
            DropColumn("dbo.AcceptPendings", "DELVNO");
            DropColumn("dbo.AcceptedDelays", "DELVNO");
            DropColumn("dbo.AcceptedAdjusteds", "DELVNO");
        }
    }
}
