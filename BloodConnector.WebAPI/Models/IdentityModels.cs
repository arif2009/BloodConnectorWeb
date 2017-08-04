using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using BloodConnector.WebAPI.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace BloodConnector.WebAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NikeName { get; set; }
        [Required]
        [Display(Name = "BloodGroup")]
        public int BloodGroupId { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public string AlternativeContactNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        [DisplayName("Gender")]
        public Enums.GenderType? Gender { get; set; }
        public Enums.Religion? Religion { get; set; }
        public string PersonalIdentityNum { get; set; }
        public IList<Attachment> Attachments { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<BloodGroup> BloodGroup { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<BloodTransaction> BloodTransaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var userConfig = modelBuilder.Entity<User>();
            userConfig.ToTable("User").Property(p => p.Id).HasColumnName("ID");
            userConfig.Property(p => p.FirstName).HasMaxLength(256);
            userConfig.Property(p => p.LastName).HasMaxLength(256);
            userConfig.Property(p => p.NikeName).HasMaxLength(256);
            userConfig.Property(p => p.AlternativeContactNo).HasMaxLength(128);
            userConfig.Property(p => p.PostCode).HasMaxLength(128);
            userConfig.Property(p => p.City).HasMaxLength(128);
            userConfig.Property(p => p.PersonalIdentityNum).HasMaxLength(128);
            userConfig.Property(p => p.Email).HasMaxLength(512);
            userConfig.Property(p => p.UserName).HasMaxLength(256);
            userConfig.Property(p => p.UserId).HasUniqueConstraint("UX_UserId");
            userConfig.Property(p => p.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            userConfig.HasRequired(p => p.BloodGroup).WithMany(p => p.Users);
            userConfig.HasOptional(p => p.Country).WithMany(p => p.Users);

            var countryConfig = modelBuilder.Entity<Country>();
            countryConfig.Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            countryConfig.Property(p => p.Name).HasMaxLength(128);
            countryConfig.Property(p => p.TowLetterCode).HasMaxLength(32);

            var bloodGroupConfig = modelBuilder.Entity<BloodGroup>();
            bloodGroupConfig.Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            bloodGroupConfig.Property(p => p.Symbole).HasMaxLength(64);

            var bloodTransConfig = modelBuilder.Entity<BloodTransaction>();
            bloodTransConfig.HasOptional(p => p.Donor).WithMany();
            bloodTransConfig.HasOptional(p => p.Receiver).WithMany();

            var attachConfig = modelBuilder.Entity<Attachment>();
            attachConfig.Property(p => p.FileguId).HasMaxLength(256);
            attachConfig.Property(p => p.Type).HasMaxLength(128);
            attachConfig.Property(p => p.ContentType).HasMaxLength(128);
            attachConfig.Property(p => p.FileName).HasMaxLength(256);
            attachConfig.HasRequired(p => p.User).WithMany(p => p.Attachments);

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
        }
    }
}