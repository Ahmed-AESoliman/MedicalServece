namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRatingTBL4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RatingDRs", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.RatingDRs", "userId");
            RenameColumn(table: "dbo.RatingDRs", name: "ApplicationUser_Id", newName: "UserID");
            AlterColumn("dbo.RatingDRs", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.RatingDRs", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RatingDRs", new[] { "UserID" });
            AlterColumn("dbo.RatingDRs", "UserID", c => c.String());
            RenameColumn(table: "dbo.RatingDRs", name: "UserID", newName: "ApplicationUser_Id");
            AddColumn("dbo.RatingDRs", "userId", c => c.String());
            CreateIndex("dbo.RatingDRs", "ApplicationUser_Id");
        }
    }
}
