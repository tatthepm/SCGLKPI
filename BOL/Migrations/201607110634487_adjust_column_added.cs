namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjust_column_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcceptedAdjusteds", "ACPD_REASON_ID", c => c.Int());
            AddColumn("dbo.AcceptedAdjusteds", "ACPD_REASON", c => c.String());
            AddColumn("dbo.AcceptedAdjusteds", "ACPD_ADJUST", c => c.Int());
            AddColumn("dbo.AcceptedAdjusteds", "ACPD_ADJUST_BY", c => c.String(maxLength: 100));
            AddColumn("dbo.AcceptedAdjusteds", "ACPD_ADJUST_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AcceptedAdjusteds", "ACPD_REMARK", c => c.String());
            AddColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_REASON_ID", c => c.Int());
            AddColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_REASON", c => c.String());
            AddColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_ADJUST", c => c.Int());
            AddColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_ADJUST_BY", c => c.String(maxLength: 100));
            AddColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_ADJUST_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_REMARK", c => c.String());
            AddColumn("dbo.InboundAdjusteds", "INB_REASON_ID", c => c.Int());
            AddColumn("dbo.InboundAdjusteds", "INB_REASON", c => c.String());
            AddColumn("dbo.InboundAdjusteds", "INB_ADJUST", c => c.Int());
            AddColumn("dbo.InboundAdjusteds", "INB_ADJUST_BY", c => c.String(maxLength: 100));
            AddColumn("dbo.InboundAdjusteds", "INB_ADJUST_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.InboundAdjusteds", "INB_REMARK", c => c.String());
            AddColumn("dbo.OntimeAdjusteds", "ON_TIME_REASON_ID", c => c.Int());
            AddColumn("dbo.OntimeAdjusteds", "ON_TIME_REASON", c => c.String());
            AddColumn("dbo.OntimeAdjusteds", "ON_TIME_ADJUST", c => c.Int());
            AddColumn("dbo.OntimeAdjusteds", "ON_TIME_ADJUST_BY", c => c.String(maxLength: 100));
            AddColumn("dbo.OntimeAdjusteds", "ON_TIME_ADJUST_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OntimeAdjusteds", "ON_TIME_REMARK", c => c.String());
            AddColumn("dbo.OutboundAdjusteds", "OUTB_REASON_ID", c => c.Int());
            AddColumn("dbo.OutboundAdjusteds", "OUTB_REASON", c => c.String());
            AddColumn("dbo.OutboundAdjusteds", "OUTB_ADJUST", c => c.Int());
            AddColumn("dbo.OutboundAdjusteds", "OUTB_ADJUST_BY", c => c.String(maxLength: 100));
            AddColumn("dbo.OutboundAdjusteds", "OUTB_ADJUST_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.OutboundAdjusteds", "OUTB_REMARK", c => c.String());
            AddColumn("dbo.TenderedAdjusteds", "TNRD_ONTIME_REASON_ID", c => c.Int());
            AddColumn("dbo.TenderedAdjusteds", "TNRD_ONTIME_REASON", c => c.String());
            AddColumn("dbo.TenderedAdjusteds", "TNRD_ADJUST", c => c.Int());
            AddColumn("dbo.TenderedAdjusteds", "TNRD_ADJUST_BY", c => c.String(maxLength: 100));
            AddColumn("dbo.TenderedAdjusteds", "TNRD_ADJUST_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TenderedAdjusteds", "TNRD_ONTIME_REMARK", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderedAdjusteds", "TNRD_ONTIME_REMARK");
            DropColumn("dbo.TenderedAdjusteds", "TNRD_ADJUST_DATE");
            DropColumn("dbo.TenderedAdjusteds", "TNRD_ADJUST_BY");
            DropColumn("dbo.TenderedAdjusteds", "TNRD_ADJUST");
            DropColumn("dbo.TenderedAdjusteds", "TNRD_ONTIME_REASON");
            DropColumn("dbo.TenderedAdjusteds", "TNRD_ONTIME_REASON_ID");
            DropColumn("dbo.OutboundAdjusteds", "OUTB_REMARK");
            DropColumn("dbo.OutboundAdjusteds", "OUTB_ADJUST_DATE");
            DropColumn("dbo.OutboundAdjusteds", "OUTB_ADJUST_BY");
            DropColumn("dbo.OutboundAdjusteds", "OUTB_ADJUST");
            DropColumn("dbo.OutboundAdjusteds", "OUTB_REASON");
            DropColumn("dbo.OutboundAdjusteds", "OUTB_REASON_ID");
            DropColumn("dbo.OntimeAdjusteds", "ON_TIME_REMARK");
            DropColumn("dbo.OntimeAdjusteds", "ON_TIME_ADJUST_DATE");
            DropColumn("dbo.OntimeAdjusteds", "ON_TIME_ADJUST_BY");
            DropColumn("dbo.OntimeAdjusteds", "ON_TIME_ADJUST");
            DropColumn("dbo.OntimeAdjusteds", "ON_TIME_REASON");
            DropColumn("dbo.OntimeAdjusteds", "ON_TIME_REASON_ID");
            DropColumn("dbo.InboundAdjusteds", "INB_REMARK");
            DropColumn("dbo.InboundAdjusteds", "INB_ADJUST_DATE");
            DropColumn("dbo.InboundAdjusteds", "INB_ADJUST_BY");
            DropColumn("dbo.InboundAdjusteds", "INB_ADJUST");
            DropColumn("dbo.InboundAdjusteds", "INB_REASON");
            DropColumn("dbo.InboundAdjusteds", "INB_REASON_ID");
            DropColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_REMARK");
            DropColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_ADJUST_DATE");
            DropColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_ADJUST_BY");
            DropColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_ADJUST");
            DropColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_REASON");
            DropColumn("dbo.DocReturnAdjusteds", "SCGL_DOCRET_REASON_ID");
            DropColumn("dbo.AcceptedAdjusteds", "ACPD_REMARK");
            DropColumn("dbo.AcceptedAdjusteds", "ACPD_ADJUST_DATE");
            DropColumn("dbo.AcceptedAdjusteds", "ACPD_ADJUST_BY");
            DropColumn("dbo.AcceptedAdjusteds", "ACPD_ADJUST");
            DropColumn("dbo.AcceptedAdjusteds", "ACPD_REASON");
            DropColumn("dbo.AcceptedAdjusteds", "ACPD_REASON_ID");
        }
    }
}
