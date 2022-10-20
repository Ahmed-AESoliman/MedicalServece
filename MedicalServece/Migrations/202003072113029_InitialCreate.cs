namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Radiationcategories",
                c => new
                    {
                        Rcategory_Code = c.Int(nullable: false, identity: true),
                        RC_category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Rcategory_Code);
            
            CreateTable(
                "dbo.RadiationLabContents",
                c => new
                    {
                        RC_Code = c.Int(nullable: false, identity: true),
                        RC_Content = c.String(nullable: false),
                        Rcategory_Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RC_Code)
                .ForeignKey("dbo.Radiationcategories", t => t.Rcategory_Code, cascadeDelete: true)
                .Index(t => t.Rcategory_Code);
            
            CreateTable(
                "dbo.RadiationLabs",
                c => new
                    {
                        R_Code = c.Int(nullable: false, identity: true),
                        R_Name = c.String(nullable: false),
                        R_Phone = c.String(nullable: false),
                        R_Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.R_Code);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Testscategories",
                c => new
                    {
                        Tcategory_Code = c.Int(nullable: false, identity: true),
                        TC_category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Tcategory_Code);
            
            CreateTable(
                "dbo.TestsLabContents",
                c => new
                    {
                        TC_Code = c.Int(nullable: false, identity: true),
                        TC_Content = c.String(nullable: false),
                        Tcategory_Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TC_Code)
                .ForeignKey("dbo.Testscategories", t => t.Tcategory_Code, cascadeDelete: true)
                .Index(t => t.Tcategory_Code);
            
            CreateTable(
                "dbo.TestsLabs",
                c => new
                    {
                        T_Code = c.Int(nullable: false, identity: true),
                        T_Name = c.String(nullable: false),
                        T_Phone = c.String(nullable: false),
                        T_Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.T_Code);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RadiationLabRadiationLabContents",
                c => new
                    {
                        RadiationLab_R_Code = c.Int(nullable: false),
                        RadiationLabContent_RC_Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RadiationLab_R_Code, t.RadiationLabContent_RC_Code })
                .ForeignKey("dbo.RadiationLabs", t => t.RadiationLab_R_Code, cascadeDelete: true)
                .ForeignKey("dbo.RadiationLabContents", t => t.RadiationLabContent_RC_Code, cascadeDelete: true)
                .Index(t => t.RadiationLab_R_Code)
                .Index(t => t.RadiationLabContent_RC_Code);
            
            CreateTable(
                "dbo.TestsLabTestsLabContents",
                c => new
                    {
                        TestsLab_T_Code = c.Int(nullable: false),
                        TestsLabContent_TC_Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestsLab_T_Code, t.TestsLabContent_TC_Code })
                .ForeignKey("dbo.TestsLabs", t => t.TestsLab_T_Code, cascadeDelete: true)
                .ForeignKey("dbo.TestsLabContents", t => t.TestsLabContent_TC_Code, cascadeDelete: true)
                .Index(t => t.TestsLab_T_Code)
                .Index(t => t.TestsLabContent_TC_Code);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TestsLabTestsLabContents", "TestsLabContent_TC_Code", "dbo.TestsLabContents");
            DropForeignKey("dbo.TestsLabTestsLabContents", "TestsLab_T_Code", "dbo.TestsLabs");
            DropForeignKey("dbo.TestsLabContents", "Tcategory_Code", "dbo.Testscategories");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RadiationLabContents", "Rcategory_Code", "dbo.Radiationcategories");
            DropForeignKey("dbo.RadiationLabRadiationLabContents", "RadiationLabContent_RC_Code", "dbo.RadiationLabContents");
            DropForeignKey("dbo.RadiationLabRadiationLabContents", "RadiationLab_R_Code", "dbo.RadiationLabs");
            DropIndex("dbo.TestsLabTestsLabContents", new[] { "TestsLabContent_TC_Code" });
            DropIndex("dbo.TestsLabTestsLabContents", new[] { "TestsLab_T_Code" });
            DropIndex("dbo.RadiationLabRadiationLabContents", new[] { "RadiationLabContent_RC_Code" });
            DropIndex("dbo.RadiationLabRadiationLabContents", new[] { "RadiationLab_R_Code" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TestsLabContents", new[] { "Tcategory_Code" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RadiationLabContents", new[] { "Rcategory_Code" });
            DropTable("dbo.TestsLabTestsLabContents");
            DropTable("dbo.RadiationLabRadiationLabContents");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TestsLabs");
            DropTable("dbo.TestsLabContents");
            DropTable("dbo.Testscategories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RadiationLabs");
            DropTable("dbo.RadiationLabContents");
            DropTable("dbo.Radiationcategories");
        }
    }
}
