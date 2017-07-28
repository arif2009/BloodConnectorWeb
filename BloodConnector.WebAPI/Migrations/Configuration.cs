using BloodConnector.WebAPI.Models;

namespace BloodConnector.WebAPI.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.BloodGroup.AddOrUpdate(
                new BloodGroup { ID = 1, Symbole = "O−" },
                new BloodGroup { ID = 2, Symbole = "O+" },
                new BloodGroup { ID = 3, Symbole = "A−" },
                new BloodGroup { ID = 4, Symbole = "A+" },
                new BloodGroup { ID = 5, Symbole = "B−" },
                new BloodGroup { ID = 6, Symbole = "B+" },
                new BloodGroup { ID = 7, Symbole = "AB−" },
                new BloodGroup { ID = 8, Symbole = "AB+" }
            );

            context.Country.AddOrUpdate(
                new Country {ID = 1, Name = "asd", TowLetterCode = "as", PhonePrefix = "88"}
            );

            base.Seed(context);
        }
    }
}
