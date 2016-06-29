namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOutboundDelay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcceptedDelays", "CARRIER_ID", c => c.String(maxLength: 32));
            AlterColumn("dbo.InboundDelays", "CARRIER_ID", c => c.String(maxLength: 32));
            AlterColumn("dbo.TenderedDelays", "CARRIER_ID", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TenderedDelays", "CARRIER_ID", c => c.Int());
            AlterColumn("dbo.InboundDelays", "CARRIER_ID", c => c.Int());
            AlterColumn("dbo.AcceptedDelays", "CARRIER_ID", c => c.Int());
        }
    }
}
