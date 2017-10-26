namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueEmailConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false, maxLength: 512));
            CreateIndex("dbo.AspNetUsers", "Email", unique: true, name: "UX_Email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", "UX_Email");
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 512));
        }
    }
}
