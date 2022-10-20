namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNerssingTBl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NCheckUps",
                c => new
                    {
                        N_ID = c.Int(nullable: false, identity: true),
                        CriminalFile = c.String(nullable: false),
                        BloodTest = c.String(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.N_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            AddColumn("dbo.AspNetUsers", "NActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NCheckUps", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.NCheckUps", new[] { "UserID" });
            DropColumn("dbo.AspNetUsers", "NActive");
            DropTable("dbo.NCheckUps");
        }
    }
}
