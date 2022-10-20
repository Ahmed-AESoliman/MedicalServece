namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRatingTBL2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RatingDRs", "clinic_C_Id", "dbo.Clinics");
            DropIndex("dbo.RatingDRs", new[] { "clinic_C_Id" });
            RenameColumn(table: "dbo.RatingDRs", name: "clinic_C_Id", newName: "C_id");
            AlterColumn("dbo.RatingDRs", "C_id", c => c.Int(nullable: false));
            CreateIndex("dbo.RatingDRs", "C_id");
            AddForeignKey("dbo.RatingDRs", "C_id", "dbo.Clinics", "C_Id", cascadeDelete: true);
            DropColumn("dbo.RatingDRs", "CL_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RatingDRs", "CL_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.RatingDRs", "C_id", "dbo.Clinics");
            DropIndex("dbo.RatingDRs", new[] { "C_id" });
            AlterColumn("dbo.RatingDRs", "C_id", c => c.Int());
            RenameColumn(table: "dbo.RatingDRs", name: "C_id", newName: "clinic_C_Id");
            CreateIndex("dbo.RatingDRs", "clinic_C_Id");
            AddForeignKey("dbo.RatingDRs", "clinic_C_Id", "dbo.Clinics", "C_Id");
        }
    }
}
