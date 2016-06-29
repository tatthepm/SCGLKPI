namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldLoaddedDate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcceptedDelays", "LOADED_DATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcceptedDelays", "LOADED_DATE");
        }
    }
}
