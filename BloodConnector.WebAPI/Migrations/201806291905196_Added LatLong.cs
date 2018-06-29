namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatLong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LatLong", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LatLong");
        }
    }
}
