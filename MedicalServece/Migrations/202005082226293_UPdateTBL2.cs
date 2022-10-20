namespace MedicalServece.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdateTBL2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RatingDRs", "NRID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RatingDRs", "NRID");
        }
    }
}
