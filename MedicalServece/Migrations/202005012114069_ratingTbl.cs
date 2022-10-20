namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingDRs",
                c => new
                    {
                        ratingID = c.Int(nullable: false, identity: true),
                        conment = c.String(),
                        rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        datepost = c.DateTime(nullable: false),
                        DrId = c.String(),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ratingID)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RatingDRs", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.RatingDRs", new[] { "userId" });
            DropTable("dbo.RatingDRs");
        }
    }
}
