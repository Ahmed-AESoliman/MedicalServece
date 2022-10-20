namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestResultTBL01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResultFiles",
                c => new
                    {
                        R_id = c.Int(nullable: false, identity: true),
                        Result = c.String(),
                        InserDate = c.DateTime(nullable: false),
                        T_id = c.Int(nullable: false),
                        F_id = c.Int(nullable: false),
                        resultFile_R_id = c.Int(),
                    })
                .PrimaryKey(t => t.R_id)
                .ForeignKey("dbo.MFiles", t => t.F_id, cascadeDelete: true)
                .ForeignKey("dbo.ResultFiles", t => t.resultFile_R_id)
                .ForeignKey("dbo.TestsLists", t => t.T_id, cascadeDelete: true)
                .Index(t => t.T_id)
                .Index(t => t.F_id)
                .Index(t => t.resultFile_R_id);
            
            CreateTable(
                "dbo.TestCategories",
                c => new
                    {
                        Cat_id = c.Int(nullable: false, identity: true),
                        Cat_name = c.String(),
                    })
                .PrimaryKey(t => t.Cat_id);
            
            CreateTable(
                "dbo.TestsLists",
                c => new
                    {
                        T_id = c.Int(nullable: false, identity: true),
                        T_name = c.String(),
                        T_unit = c.String(),
                        T_refRange = c.String(),
                        Cat_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.T_id)
                .ForeignKey("dbo.TestCategories", t => t.Cat_id, cascadeDelete: true)
                .Index(t => t.Cat_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestsLists", "Cat_id", "dbo.TestCategories");
            DropForeignKey("dbo.ResultFiles", "T_id", "dbo.TestsLists");
            DropForeignKey("dbo.ResultFiles", "resultFile_R_id", "dbo.ResultFiles");
            DropForeignKey("dbo.ResultFiles", "F_id", "dbo.MFiles");
            DropIndex("dbo.TestsLists", new[] { "Cat_id" });
            DropIndex("dbo.ResultFiles", new[] { "resultFile_R_id" });
            DropIndex("dbo.ResultFiles", new[] { "F_id" });
            DropIndex("dbo.ResultFiles", new[] { "T_id" });
            DropTable("dbo.TestsLists");
            DropTable("dbo.TestCategories");
            DropTable("dbo.ResultFiles");
        }
    }
}
