namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        NikeName = c.String(maxLength: 256),
                        BloodGroupId = c.Int(nullable: false),
                        AlternativeContactNo = c.String(maxLength: 128),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                        PostCode = c.String(maxLength: 128),
                        City = c.String(maxLength: 128),
                        CountryId = c.Int(nullable: false),
                        Gender = c.Int(),
                        Religion = c.Int(),
                        PersonalIdentityNum = c.String(maxLength: 128),
                        Email = c.String(maxLength: 512),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Country_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BloodGroup", t => t.BloodGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.Country_ID, cascadeDelete: true)
                .Index(t => t.UserId, unique: true, name: "UX_UserId")
                .Index(t => t.BloodGroupId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Country_ID);
            
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
            
            CreateTable(
                "dbo.BloodGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Symbole = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        TowLetterCode = c.String(maxLength: 32),
                        PhonePrefix = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "Country_ID", "dbo.Country");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "BloodGroupId", "dbo.BloodGroup");
            DropForeignKey("dbo.Attachment", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropIndex("dbo.BloodTransaction", new[] { "ReceiverId" });
            DropIndex("dbo.BloodTransaction", new[] { "DonorId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.Attachment", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "Country_ID" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.User", new[] { "BloodGroupId" });
            DropIndex("dbo.User", "UX_UserId");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropTable("dbo.BloodTransaction");
            DropTable("dbo.UserLogin");
            DropTable("dbo.Country");
            DropTable("dbo.UserClaim");
            DropTable("dbo.BloodGroup");
            DropTable("dbo.Attachment");
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
        }
    }
}
