namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "Birthdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ProfessionalTitle", c => c.String());
            AddColumn("dbo.AspNetUsers", "FullProfessionalTitle", c => c.String());
            AddColumn("dbo.AspNetUsers", "ImageUser", c => c.String());
            AddColumn("dbo.AspNetUsers", "Summray", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "Degree", c => c.String());
            AddColumn("dbo.AspNetUsers", "ExYear", c => c.String());
            AddColumn("dbo.AspNetUsers", "MajorSpecialization", c => c.String());
            AddColumn("dbo.AspNetUsers", "SubSpecialization", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SubSpecialization");
            DropColumn("dbo.AspNetUsers", "MajorSpecialization");
            DropColumn("dbo.AspNetUsers", "ExYear");
            DropColumn("dbo.AspNetUsers", "Degree");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "Summray");
            DropColumn("dbo.AspNetUsers", "ImageUser");
            DropColumn("dbo.AspNetUsers", "FullProfessionalTitle");
            DropColumn("dbo.AspNetUsers", "ProfessionalTitle");
            DropColumn("dbo.AspNetUsers", "Birthdate");
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}
