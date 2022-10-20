namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addClinicTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        A_id = c.Int(nullable: false, identity: true),
                        Day = c.String(nullable: false),
                        TimeFrom = c.String(nullable: false),
                        TimeTo = c.String(nullable: false),
                        C_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.A_id)
                .ForeignKey("dbo.Clinics", t => t.C_id, cascadeDelete: true)
                .Index(t => t.C_id);
            
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        C_Id = c.Int(nullable: false, identity: true),
                        ClinicName = c.String(nullable: false),
                        ClininPhone = c.String(nullable: false),
                        BookingPrice = c.String(nullable: false),
                        City = c.String(nullable: false),
                        ClinicArea = c.String(nullable: false),
                        ClinicStreat = c.String(nullable: false),
                        BuldingNumber = c.String(nullable: false),
                        Floor = c.String(nullable: false),
                        SpecialMarque = c.String(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.C_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clinics", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "C_id", "dbo.Clinics");
            DropIndex("dbo.Clinics", new[] { "UserID" });
            DropIndex("dbo.Appointments", new[] { "C_id" });
            DropTable("dbo.Clinics");
            DropTable("dbo.Appointments");
        }
    }
}
