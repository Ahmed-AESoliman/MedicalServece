namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestResultTBL02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResultFiles", "resultFile_R_id", "dbo.ResultFiles");
            DropIndex("dbo.ResultFiles", new[] { "resultFile_R_id" });
            DropColumn("dbo.ResultFiles", "resultFile_R_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultFiles", "resultFile_R_id", c => c.Int());
            CreateIndex("dbo.ResultFiles", "resultFile_R_id");
            AddForeignKey("dbo.ResultFiles", "resultFile_R_id", "dbo.ResultFiles", "R_id");
        }
    }
}
