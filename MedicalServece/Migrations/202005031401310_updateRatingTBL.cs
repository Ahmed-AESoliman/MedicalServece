namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRatingTBL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RatingDRs", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.RatingDRs", new[] { "userId" });
            AddColumn("dbo.RatingDRs", "C_id", c => c.Int(nullable: false));
            AddColumn("dbo.RatingDRs", "clinic_C_Id", c => c.Int());
            AlterColumn("dbo.RatingDRs", "userId", c => c.String());
            CreateIndex("dbo.RatingDRs", "clinic_C_Id");
            AddForeignKey("dbo.RatingDRs", "clinic_C_Id", "dbo.Clinics", "C_Id");
            DropColumn("dbo.RatingDRs", "DrId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RatingDRs", "DrId", c => c.String());
            DropForeignKey("dbo.RatingDRs", "clinic_C_Id", "dbo.Clinics");
            DropIndex("dbo.RatingDRs", new[] { "clinic_C_Id" });
            AlterColumn("dbo.RatingDRs", "userId", c => c.String(maxLength: 128));
            DropColumn("dbo.RatingDRs", "clinic_C_Id");
            DropColumn("dbo.RatingDRs", "CL_id");
            CreateIndex("dbo.RatingDRs", "userId");
            AddForeignKey("dbo.RatingDRs", "userId", "dbo.AspNetUsers", "Id");
        }
    }
}
