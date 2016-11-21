namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadPLNDELVtoPendDocRtn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocReturnPendings", "PLDLVDATE", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocReturnPendings", "PLDLVDATE");
        }
    }
}
