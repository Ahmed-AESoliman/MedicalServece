namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTBLTests04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultFiles", "TC_Code", c => c.Int(nullable: false));
            CreateIndex("dbo.ResultFiles", "TC_Code");
            AddForeignKey("dbo.ResultFiles", "TC_Code", "dbo.TestsLabContents", "TC_Code", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultFiles", "TC_Code", "dbo.TestsLabContents");
            DropIndex("dbo.ResultFiles", new[] { "TC_Code" });
            DropColumn("dbo.ResultFiles", "TC_Code");
        }
    }
}
