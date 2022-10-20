namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upSpecializationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MajorSpecializations",
                c => new
                    {
                        Major_ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Major_ID);
            
            CreateTable(
                "dbo.SubSpecializations",
                c => new
                    {
                        Sub_ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Major_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sub_ID)
                .ForeignKey("dbo.MajorSpecializations", t => t.Major_ID, cascadeDelete: true)
                .Index(t => t.Major_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubSpecializations", "Major_ID", "dbo.MajorSpecializations");
            DropIndex("dbo.SubSpecializations", new[] { "Major_ID" });
            DropTable("dbo.SubSpecializations");
            DropTable("dbo.MajorSpecializations");
        }
    }
}
