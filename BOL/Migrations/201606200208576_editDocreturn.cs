namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDocreturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OntimeDocReturnMonths", "AdjustDocReturn", c => c.Int(nullable: false));
            DropColumn("dbo.OntimeDocReturnMonths", "AdjustDocRetunr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OntimeDocReturnMonths", "AdjustDocRetunr", c => c.Int(nullable: false));
            DropColumn("dbo.OntimeDocReturnMonths", "AdjustDocReturn");
        }
    }
}
