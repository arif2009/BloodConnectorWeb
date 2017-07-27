namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBloodTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloodTransaction",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DonorId = c.String(maxLength: 128),
                        ReceiverId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.DonorId)
                .ForeignKey("dbo.User", t => t.ReceiverId)
                .Index(t => t.DonorId)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BloodTransaction", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.BloodTransaction", "DonorId", "dbo.User");
            DropIndex("dbo.BloodTransaction", new[] { "ReceiverId" });
            DropIndex("dbo.BloodTransaction", new[] { "DonorId" });
            DropTable("dbo.BloodTransaction");
        }
    }
}
