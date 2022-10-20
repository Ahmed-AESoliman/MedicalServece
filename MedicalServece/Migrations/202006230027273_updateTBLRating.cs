namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTBLRating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RatingDRs", "C_id", "dbo.Clinics");
            DropForeignKey("dbo.RatingDRs", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.RatingDRs", new[] { "UserID" });
            DropIndex("dbo.RatingDRs", new[] { "C_id" });
            DropTable("dbo.RatingDRs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RatingDRs",
                c => new
                    {
                        ratingID = c.Int(nullable: false, identity: true),
                        conment = c.String(),
                        rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        datepost = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        C_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ratingID);
            
            CreateIndex("dbo.RatingDRs", "C_id");
            CreateIndex("dbo.RatingDRs", "UserID");
            AddForeignKey("dbo.RatingDRs", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RatingDRs", "C_id", "dbo.Clinics", "C_Id", cascadeDelete: true);
        }
    }
}
