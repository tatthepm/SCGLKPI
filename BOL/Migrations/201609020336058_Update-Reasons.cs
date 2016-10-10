namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReasons : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReasonAccepteds", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonDocReturns", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonInbounds", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonOntimes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReasonOutbounds", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.EquipmentTypess", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EquipmentTypess", "IsDeleted");
            DropColumn("dbo.ReasonOutbounds", "IsDeleted");
            DropColumn("dbo.ReasonOntimes", "IsDeleted");
            DropColumn("dbo.ReasonInbounds", "IsDeleted");
            DropColumn("dbo.ReasonDocReturns", "IsDeleted");
            DropColumn("dbo.ReasonAccepteds", "IsDeleted");
        }
    }
}
