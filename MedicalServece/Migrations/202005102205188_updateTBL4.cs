namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTBL4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RatingNRs", "UserName", c => c.String());
            DropColumn("dbo.RatingNRs", "NRID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RatingNRs", "NRID", c => c.String());
            DropColumn("dbo.RatingNRs", "UserName");
        }
    }
}
