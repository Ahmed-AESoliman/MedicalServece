namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFileTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileContents",
                c => new
                    {
                        FC_id = c.Int(nullable: false, identity: true),
                        Cat_file = c.String(nullable: false),
                        filePath = c.String(nullable: false),
                        F_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FC_id)
                .ForeignKey("dbo.MFiles", t => t.F_id, cascadeDelete: true)
                .Index(t => t.F_id);
            
            CreateTable(
                "dbo.MFiles",
                c => new
                    {
                        F_id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.F_id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MFiles", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FileContents", "F_id", "dbo.MFiles");
            DropIndex("dbo.MFiles", new[] { "UserID" });
            DropIndex("dbo.FileContents", new[] { "F_id" });
            DropTable("dbo.MFiles");
            DropTable("dbo.FileContents");
        }
    }
}
