namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PLNDELVtoPLNONTIME : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocReturnPendings", "PLNONTIMEDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.DocReturnPendings", "PLDLVDATE");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DocReturnPendings", "PLDLVDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.DocReturnPendings", "PLNONTIMEDATE");
        }
    }
}
