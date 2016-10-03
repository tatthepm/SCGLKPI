namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcceptedFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SHPMNTNO = c.String(maxLength: 10),
                        FILEPATH = c.String(maxLength: 800),
                        REMARK = c.String(maxLength: 800),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_BY = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.SHPMNTNO);
            
            CreateTable(
                "dbo.DocReturnFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DELVNO = c.String(maxLength: 10),
                        FILEPATH = c.String(maxLength: 800),
                        REMARK = c.String(maxLength: 800),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_BY = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.DELVNO);
            
            CreateTable(
                "dbo.InboundedFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DELVNO = c.String(maxLength: 10),
                        FILEPATH = c.String(maxLength: 800),
                        REMARK = c.String(maxLength: 800),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_BY = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.DELVNO);
            
            CreateTable(
                "dbo.OntimeFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DELVNO = c.String(maxLength: 10),
                        FILEPATH = c.String(maxLength: 800),
                        REMARK = c.String(maxLength: 800),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_BY = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.DELVNO);
            
            CreateTable(
                "dbo.OutboundedFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DELVNO = c.String(maxLength: 10),
                        FILEPATH = c.String(maxLength: 800),
                        REMARK = c.String(maxLength: 800),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_BY = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.DELVNO);
            
            CreateTable(
                "dbo.TenderedFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SHPMNTNO = c.String(maxLength: 10),
                        FILEPATH = c.String(maxLength: 800),
                        REMARK = c.String(maxLength: 800),
                        LOADED_DATE = c.DateTime(precision: 7, storeType: "datetime2"),
                        LOADED_BY = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.SHPMNTNO);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TenderedFiles", new[] { "SHPMNTNO" });
            DropIndex("dbo.OutboundedFiles", new[] { "DELVNO" });
            DropIndex("dbo.OntimeFiles", new[] { "DELVNO" });
            DropIndex("dbo.InboundedFiles", new[] { "DELVNO" });
            DropIndex("dbo.DocReturnFiles", new[] { "DELVNO" });
            DropIndex("dbo.AcceptedFiles", new[] { "SHPMNTNO" });
            DropTable("dbo.TenderedFiles");
            DropTable("dbo.OutboundedFiles");
            DropTable("dbo.OntimeFiles");
            DropTable("dbo.InboundedFiles");
            DropTable("dbo.DocReturnFiles");
            DropTable("dbo.AcceptedFiles");
        }
    }
}
