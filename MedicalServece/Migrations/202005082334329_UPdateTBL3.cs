namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdateTBL3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingNRs",
                c => new
                    {
                        ratingID = c.Int(nullable: false, identity: true),
                        conment = c.String(),
                        rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        datepost = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        NRID = c.String(),
                    })
                .PrimaryKey(t => t.ratingID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            DropColumn("dbo.RatingDRs", "NRID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RatingDRs", "NRID", c => c.String());
            DropForeignKey("dbo.RatingNRs", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.RatingNRs", new[] { "UserID" });
            DropTable("dbo.RatingNRs");
        }
    }
}
