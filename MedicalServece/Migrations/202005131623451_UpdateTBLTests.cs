namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTBLTests : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestsLists", "Cat_id", "dbo.TestCategories");
            DropIndex("dbo.TestsLists", new[] { "Cat_id" });
            RenameColumn(table: "dbo.ResultFiles", name: "T_id", newName: "TC_Code");
            RenameIndex(table: "dbo.ResultFiles", name: "IX_T_id", newName: "IX_TC_Code");
            AddColumn("dbo.TestsLabContents", "T_unit", c => c.String());
            AddColumn("dbo.TestsLabContents", "T_refRange", c => c.String());
            DropTable("dbo.TestsLists");
            DropTable("dbo.TestCategories");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.T_id);
            
            DropColumn("dbo.TestsLabContents", "T_refRange");
            DropColumn("dbo.TestsLabContents", "T_unit");
            RenameIndex(table: "dbo.ResultFiles", name: "IX_TC_Code", newName: "IX_T_id");
            RenameColumn(table: "dbo.ResultFiles", name: "TC_Code", newName: "T_id");
            CreateIndex("dbo.TestsLists", "Cat_id");
            AddForeignKey("dbo.TestsLists", "Cat_id", "dbo.TestCategories", "Cat_id", cascadeDelete: true);
        }
    }
}
