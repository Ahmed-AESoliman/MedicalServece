namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserTable00 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FullUserName");
        }
    }
}
