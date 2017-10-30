namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableDatabaseGeneratedOption : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UpdatedDate", c => c.DateTime(nullable: false));
        }
    }
}
