namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColBloodGiven : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BloodGiven", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BloodGiven");
        }
    }
}
