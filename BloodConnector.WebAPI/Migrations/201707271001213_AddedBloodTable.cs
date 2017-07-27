namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBloodTable : DbMigration
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
                        FirstName = c.String(),
                        LastName = c.String(),
                        NikeName = c.String(),
                        BloodGroupId = c.Int(nullable: false),
                        AlternativeContactNo = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                        PostCode = c.String(),
                        City = c.String(),
                        CountryId = c.Int(nullable: false),
                        Gender = c.Int(),
                        PersonalIdentityNum = c.String(),
                        Email = c.String(maxLength: 256),
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
                .Index(t => t.UserId, unique: true)
                .Index(t => t.BloodGroupId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Country_ID);
            
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
                        Name = c.String(),
                        TowLetterCode = c.String(),
                        PhonePrefix = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "Country_ID", "dbo.Country");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "BloodGroupId", "dbo.BloodGroup");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "Country_ID" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.User", new[] { "BloodGroupId" });
            DropIndex("dbo.User", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropTable("dbo.UserLogin");
            DropTable("dbo.Country");
            DropTable("dbo.UserClaim");
            DropTable("dbo.BloodGroup");
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
        }
    }
}
