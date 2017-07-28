namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "Country_ID", "dbo.Country");
            DropIndex("dbo.User", new[] { "Country_ID" });
            DropColumn("dbo.User", "CountryId");
            RenameColumn(table: "dbo.User", name: "Country_ID", newName: "CountryId");
            DropPrimaryKey("dbo.Country");
            AlterColumn("dbo.User", "CountryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Country", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Country", "ID");
            CreateIndex("dbo.User", "CountryId");
            AddForeignKey("dbo.User", "CountryId", "dbo.Country", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "CountryId", "dbo.Country");
            DropIndex("dbo.User", new[] { "CountryId" });
            DropPrimaryKey("dbo.Country");
            AlterColumn("dbo.Country", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.User", "CountryId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Country", "ID");
            RenameColumn(table: "dbo.User", name: "CountryId", newName: "Country_ID");
            AddColumn("dbo.User", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "Country_ID");
            AddForeignKey("dbo.User", "Country_ID", "dbo.Country", "ID", cascadeDelete: true);
        }
    }
}
