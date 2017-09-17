namespace BloodConnector.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        FileguId = c.String(maxLength: 256),
                        Type = c.String(maxLength: 128),
                        FileName = c.String(maxLength: 256),
                        ContentType = c.String(maxLength: 128),
                        Size = c.Long(nullable: false),
                        LastUpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        NikeName = c.String(maxLength: 256),
                        BloodGroupId = c.Int(nullable: false),
                        AlternativeContactNo = c.String(maxLength: 128),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(),
                        PostCode = c.String(maxLength: 128),
                        City = c.String(maxLength: 128),
                        CountryId = c.Int(),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BloodGroup", t => t.BloodGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.UserId, unique: true, name: "UX_UserId")
                .Index(t => t.BloodGroupId)
                .Index(t => t.CountryId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.BloodGroup",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Symbole = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        TowLetterCode = c.String(maxLength: 32),
                        PhonePrefix = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BloodTransaction",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DonorId = c.String(maxLength: 128),
                        ReceiverId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.DonorId)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverId)
                .Index(t => t.DonorId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BloodTransaction", "ReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BloodTransaction", "DonorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attachment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Country");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BloodGroupId", "dbo.BloodGroup");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BloodTransaction", new[] { "ReceiverId" });
            DropIndex("dbo.BloodTransaction", new[] { "DonorId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "BloodGroupId" });
            DropIndex("dbo.AspNetUsers", "UX_UserId");
            DropIndex("dbo.Attachment", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.BloodTransaction");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Country");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.BloodGroup");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Attachment");
        }
    }
}
