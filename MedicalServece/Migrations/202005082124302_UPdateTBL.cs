namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdateTBL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Area", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.NCheckUps", "Certificate", c => c.String());
            AddColumn("dbo.NCheckUps", "LicenseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FileContents", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileContents", "CreateDate");
            DropColumn("dbo.NCheckUps", "LicenseDate");
            DropColumn("dbo.NCheckUps", "Certificate");
            DropColumn("dbo.AspNetUsers", "IsActive");
            DropColumn("dbo.AspNetUsers", "Area");
        }
    }
}
