namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsAdjust : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReasonAccepteds", "IsAdjust", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonDocReturns", "IsAdjust", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonInbounds", "IsAdjust", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonOntimes", "IsAdjust", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonOutbounds", "IsAdjust", c => c.Boolean(nullable: false));
            AddColumn("dbo.EquipmentTypess", "IsAdjust", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EquipmentTypess", "IsAdjust");
            DropColumn("dbo.ReasonOutbounds", "IsAdjust");
            DropColumn("dbo.ReasonOntimes", "IsAdjust");
            DropColumn("dbo.ReasonInbounds", "IsAdjust");
            DropColumn("dbo.ReasonDocReturns", "IsAdjust");
            DropColumn("dbo.ReasonAccepteds", "IsAdjust");
        }
    }
}
