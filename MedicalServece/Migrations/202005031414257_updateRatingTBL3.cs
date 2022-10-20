namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRatingTBL3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RatingDRs", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.RatingDRs", "ApplicationUser_Id");
            AddForeignKey("dbo.RatingDRs", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RatingDRs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RatingDRs", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.RatingDRs", "ApplicationUser_Id");
        }
    }
}
