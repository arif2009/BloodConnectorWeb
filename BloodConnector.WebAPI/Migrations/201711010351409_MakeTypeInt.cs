namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeTypeInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attachment", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attachment", "Type", c => c.String(maxLength: 128));
        }
    }
}
