namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTender2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OntimeAcceptMonths", "DepartmentId");
            CreateIndex("dbo.OntimeAcceptMonths", "SectionId");
            CreateIndex("dbo.OntimeAccepts", "ActualGiDate");
            CreateIndex("dbo.OntimeAccepts", "DepartmentId");
            CreateIndex("dbo.OntimeAccepts", "SectionId");
            CreateIndex("dbo.OntimeAccepts", "SoldToId");
            CreateIndex("dbo.OntimeAccepts", "CarrierId");
            CreateIndex("dbo.OntimeAcceptYears", "DepartmentId");
            CreateIndex("dbo.OntimeAcceptYears", "SectionId");
            CreateIndex("dbo.OntimeAcceptYears", "SoldToId");
            CreateIndex("dbo.OntimeAcceptYears", "CarrierId");
            CreateIndex("dbo.OntimeDeliveries", "ActualGiDate");
            CreateIndex("dbo.OntimeDeliveries", "DepartmentId");
            CreateIndex("dbo.OntimeDeliveries", "SectionId");
            CreateIndex("dbo.OntimeDeliveries", "SoldToId");
            CreateIndex("dbo.OntimeDeliveries", "CarrierId");
            CreateIndex("dbo.OntimeDeliveryMonths", "DepartmentId");
            CreateIndex("dbo.OntimeDeliveryMonths", "SectionId");
            CreateIndex("dbo.OntimeDeliveryMonths", "SoldToId");
            CreateIndex("dbo.OntimeDeliveryMonths", "CarrierId");
            CreateIndex("dbo.OntimeDeliveryYears", "DepartmentId");
            CreateIndex("dbo.OntimeDeliveryYears", "SectionId");
            CreateIndex("dbo.OntimeDeliveryYears", "SoldToId");
            CreateIndex("dbo.OntimeDeliveryYears", "CarrierId");
            CreateIndex("dbo.OntimeDocReturnMonths", "DepartmentId");
            CreateIndex("dbo.OntimeDocReturnMonths", "SectionId");
            CreateIndex("dbo.OntimeDocReturnMonths", "SoldToId");
            CreateIndex("dbo.OntimeDocReturnMonths", "CarrierId");
            CreateIndex("dbo.OntimeDocReturns", "ActualGiDate");
            CreateIndex("dbo.OntimeDocReturns", "DepartmentId");
            CreateIndex("dbo.OntimeDocReturns", "SectionId");
            CreateIndex("dbo.OntimeDocReturns", "SoldToId");
            CreateIndex("dbo.OntimeDocReturns", "CarrierId");
            CreateIndex("dbo.OntimeDocReturnYears", "DepartmentId");
            CreateIndex("dbo.OntimeDocReturnYears", "SectionId");
            CreateIndex("dbo.OntimeDocReturnYears", "SoldToId");
            CreateIndex("dbo.OntimeDocReturnYears", "CarrierId");
            CreateIndex("dbo.OntimeInboundMonths", "DepartmentId");
            CreateIndex("dbo.OntimeInboundMonths", "SectionId");
            CreateIndex("dbo.OntimeInboundMonths", "SoldToId");
            CreateIndex("dbo.OntimeInboundMonths", "CarrierId");
            CreateIndex("dbo.OntimeInbounds", "ActualGiDate");
            CreateIndex("dbo.OntimeInbounds", "DepartmentId");
            CreateIndex("dbo.OntimeInbounds", "SectionId");
            CreateIndex("dbo.OntimeInbounds", "SoldToId");
            CreateIndex("dbo.OntimeInbounds", "CarrierId");
            CreateIndex("dbo.OntimeInboundYears", "DepartmentId");
            CreateIndex("dbo.OntimeInboundYears", "SectionId");
            CreateIndex("dbo.OntimeInboundYears", "SoldToId");
            CreateIndex("dbo.OntimeInboundYears", "CarrierId");
            CreateIndex("dbo.OntimeOutboundMonths", "DepartmentId");
            CreateIndex("dbo.OntimeOutboundMonths", "SectionId");
            CreateIndex("dbo.OntimeOutboundMonths", "SoldToId");
            CreateIndex("dbo.OntimeOutboundMonths", "CarrierId");
            CreateIndex("dbo.OntimeOutbounds", "ActualGiDate");
            CreateIndex("dbo.OntimeOutbounds", "DepartmentId");
            CreateIndex("dbo.OntimeOutbounds", "SectionId");
            CreateIndex("dbo.OntimeOutbounds", "SoldToId");
            CreateIndex("dbo.OntimeOutbounds", "CarrierId");
            CreateIndex("dbo.OntimeOutboundYears", "DepartmentId");
            CreateIndex("dbo.OntimeOutboundYears", "SectionId");
            CreateIndex("dbo.OntimeOutboundYears", "SoldToId");
            CreateIndex("dbo.OntimeOutboundYears", "CarrierId");
            CreateIndex("dbo.OntimeTenderMonths", "DepartmentId");
            CreateIndex("dbo.OntimeTenderMonths", "SectionId");
            CreateIndex("dbo.OntimeTenderMonths", "CRTD_USR_CD");
            CreateIndex("dbo.OntimeTenderMonths", "SoldToId");
            CreateIndex("dbo.OntimeTenderMonths", "CarrierId");
            CreateIndex("dbo.OntimeTenders", "ActualGiDate");
            CreateIndex("dbo.OntimeTenders", "DepartmentId");
            CreateIndex("dbo.OntimeTenders", "SectionId");
            CreateIndex("dbo.OntimeTenders", "CRTD_USR_CD");
            CreateIndex("dbo.OntimeTenders", "SoldToId");
            CreateIndex("dbo.OntimeTenders", "CarrierId");
            CreateIndex("dbo.OntimeTenderYears", "DepartmentId");
            CreateIndex("dbo.OntimeTenderYears", "SectionId");
            CreateIndex("dbo.OntimeTenderYears", "CRTD_USR_CD");
            CreateIndex("dbo.OntimeTenderYears", "SoldToId");
            CreateIndex("dbo.OntimeTenderYears", "CarrierId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OntimeTenderYears", new[] { "CarrierId" });
            DropIndex("dbo.OntimeTenderYears", new[] { "SoldToId" });
            DropIndex("dbo.OntimeTenderYears", new[] { "CRTD_USR_CD" });
            DropIndex("dbo.OntimeTenderYears", new[] { "SectionId" });
            DropIndex("dbo.OntimeTenderYears", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeTenders", new[] { "CarrierId" });
            DropIndex("dbo.OntimeTenders", new[] { "SoldToId" });
            DropIndex("dbo.OntimeTenders", new[] { "CRTD_USR_CD" });
            DropIndex("dbo.OntimeTenders", new[] { "SectionId" });
            DropIndex("dbo.OntimeTenders", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeTenders", new[] { "ActualGiDate" });
            DropIndex("dbo.OntimeTenderMonths", new[] { "CarrierId" });
            DropIndex("dbo.OntimeTenderMonths", new[] { "SoldToId" });
            DropIndex("dbo.OntimeTenderMonths", new[] { "CRTD_USR_CD" });
            DropIndex("dbo.OntimeTenderMonths", new[] { "SectionId" });
            DropIndex("dbo.OntimeTenderMonths", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeOutboundYears", new[] { "CarrierId" });
            DropIndex("dbo.OntimeOutboundYears", new[] { "SoldToId" });
            DropIndex("dbo.OntimeOutboundYears", new[] { "SectionId" });
            DropIndex("dbo.OntimeOutboundYears", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeOutbounds", new[] { "CarrierId" });
            DropIndex("dbo.OntimeOutbounds", new[] { "SoldToId" });
            DropIndex("dbo.OntimeOutbounds", new[] { "SectionId" });
            DropIndex("dbo.OntimeOutbounds", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeOutbounds", new[] { "ActualGiDate" });
            DropIndex("dbo.OntimeOutboundMonths", new[] { "CarrierId" });
            DropIndex("dbo.OntimeOutboundMonths", new[] { "SoldToId" });
            DropIndex("dbo.OntimeOutboundMonths", new[] { "SectionId" });
            DropIndex("dbo.OntimeOutboundMonths", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeInboundYears", new[] { "CarrierId" });
            DropIndex("dbo.OntimeInboundYears", new[] { "SoldToId" });
            DropIndex("dbo.OntimeInboundYears", new[] { "SectionId" });
            DropIndex("dbo.OntimeInboundYears", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeInbounds", new[] { "CarrierId" });
            DropIndex("dbo.OntimeInbounds", new[] { "SoldToId" });
            DropIndex("dbo.OntimeInbounds", new[] { "SectionId" });
            DropIndex("dbo.OntimeInbounds", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeInbounds", new[] { "ActualGiDate" });
            DropIndex("dbo.OntimeInboundMonths", new[] { "CarrierId" });
            DropIndex("dbo.OntimeInboundMonths", new[] { "SoldToId" });
            DropIndex("dbo.OntimeInboundMonths", new[] { "SectionId" });
            DropIndex("dbo.OntimeInboundMonths", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeDocReturnYears", new[] { "CarrierId" });
            DropIndex("dbo.OntimeDocReturnYears", new[] { "SoldToId" });
            DropIndex("dbo.OntimeDocReturnYears", new[] { "SectionId" });
            DropIndex("dbo.OntimeDocReturnYears", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeDocReturns", new[] { "CarrierId" });
            DropIndex("dbo.OntimeDocReturns", new[] { "SoldToId" });
            DropIndex("dbo.OntimeDocReturns", new[] { "SectionId" });
            DropIndex("dbo.OntimeDocReturns", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeDocReturns", new[] { "ActualGiDate" });
            DropIndex("dbo.OntimeDocReturnMonths", new[] { "CarrierId" });
            DropIndex("dbo.OntimeDocReturnMonths", new[] { "SoldToId" });
            DropIndex("dbo.OntimeDocReturnMonths", new[] { "SectionId" });
            DropIndex("dbo.OntimeDocReturnMonths", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeDeliveryYears", new[] { "CarrierId" });
            DropIndex("dbo.OntimeDeliveryYears", new[] { "SoldToId" });
            DropIndex("dbo.OntimeDeliveryYears", new[] { "SectionId" });
            DropIndex("dbo.OntimeDeliveryYears", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeDeliveryMonths", new[] { "CarrierId" });
            DropIndex("dbo.OntimeDeliveryMonths", new[] { "SoldToId" });
            DropIndex("dbo.OntimeDeliveryMonths", new[] { "SectionId" });
            DropIndex("dbo.OntimeDeliveryMonths", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeDeliveries", new[] { "CarrierId" });
            DropIndex("dbo.OntimeDeliveries", new[] { "SoldToId" });
            DropIndex("dbo.OntimeDeliveries", new[] { "SectionId" });
            DropIndex("dbo.OntimeDeliveries", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeDeliveries", new[] { "ActualGiDate" });
            DropIndex("dbo.OntimeAcceptYears", new[] { "CarrierId" });
            DropIndex("dbo.OntimeAcceptYears", new[] { "SoldToId" });
            DropIndex("dbo.OntimeAcceptYears", new[] { "SectionId" });
            DropIndex("dbo.OntimeAcceptYears", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeAccepts", new[] { "CarrierId" });
            DropIndex("dbo.OntimeAccepts", new[] { "SoldToId" });
            DropIndex("dbo.OntimeAccepts", new[] { "SectionId" });
            DropIndex("dbo.OntimeAccepts", new[] { "DepartmentId" });
            DropIndex("dbo.OntimeAccepts", new[] { "ActualGiDate" });
            DropIndex("dbo.OntimeAcceptMonths", new[] { "SectionId" });
            DropIndex("dbo.OntimeAcceptMonths", new[] { "DepartmentId" });
        }
    }
}