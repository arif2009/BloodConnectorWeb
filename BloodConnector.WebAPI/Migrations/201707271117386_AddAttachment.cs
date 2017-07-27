namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttachment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        FileguId = c.String(),
                        Type = c.String(),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Size = c.Long(nullable: false),
                        LastUpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachment", "UserId", "dbo.User");
            DropIndex("dbo.Attachment", new[] { "UserId" });
            DropTable("dbo.Attachment");
        }
    }
}
