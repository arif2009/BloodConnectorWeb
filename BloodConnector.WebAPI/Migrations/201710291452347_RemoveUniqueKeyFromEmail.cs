namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUniqueKeyFromEmail : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", "UX_Email");
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false, maxLength: 512));
            CreateIndex("dbo.AspNetUsers", "Email", unique: true, name: "UX_Email");
        }
    }
}
