namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBloodGroupName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BloodGroup", "GroupName", c => c.String(maxLength: 32));
            AlterColumn("dbo.BloodGroup", "Symbole", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BloodGroup", "Symbole", c => c.String(maxLength: 64));
            DropColumn("dbo.BloodGroup", "GroupName");
        }
    }
}
