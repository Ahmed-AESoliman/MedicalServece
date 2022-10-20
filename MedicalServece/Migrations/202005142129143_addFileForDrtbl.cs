namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFileForDrtbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileForDrs",
                c => new
                    {
                        FD_id = c.Int(nullable: false, identity: true),
                        SendDate = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Pa_URL = c.String(),
                    })
                .PrimaryKey(t => t.FD_id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.NRSpecializations",
                c => new
                    {
                        S_ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.S_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileForDrs", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.FileForDrs", new[] { "UserID" });
            DropTable("dbo.NRSpecializations");
            DropTable("dbo.FileForDrs");
        }
    }
}
