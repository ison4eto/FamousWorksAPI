namespace DataAccess.Migrations
{
    using DatabaseStructure.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.DbEntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.DbEntitiesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Composers.AddOrUpdate(new Composer[] {
                new Composer() { FirstName = "Johann Sebastian", LastName="Bach",Country="Germany"},
                new Composer() { FirstName = " Ludwig van", LastName="Bethoven",Country="Germany" },
                new Composer() { FirstName="Wolfgang Amadeus", LastName="Mozart", Country="Germany"},
                new Composer() { FirstName="Franz Peter", LastName="Schubert", Country="???" },
                new Composer() { FirstName="Claude Achille", LastName="Debussy", Country="???" },
                new Composer() { FirstName="Igor", LastName="Stravinsky", Country="Russia" },
                new Composer() { FirstName="Johanes", LastName="Brahms", Country="???" },
                new Composer() { FirstName="Giuseppe", LastName="Verdi", Country="Italy" },
                new Composer() { FirstName="Richard", LastName="Wagner", Country="Germany" },
                new Composer() { FirstName="Bela", LastName="Bartok", Country="???" },
        });

            context.Eras.AddOrUpdate(new Era[]
            {
                new Era(){Name = "Midieval"},
                new Era() {Name = "Renaissance"},
                new Era(){Name = "Baroque"},
                new Era(){Name = "Classical"},
                new Era(){Name = "Early Romantic"},
                new Era(){Name = "Late Romantic"},
                new Era(){Name = "Post 'Great War' Years"}
            });

            context.Works.AddOrUpdate(new Work[] {
        new Work() { Title= "Toccata and Fugue in D minor", EraID = 2,
            Year = 1704, ComposerID = 1, Description =
        "It's is a piece of organ music written, according to its oldest extant sources."},

        new Work() { Title = "Ave Maria", EraID = 2,
            Year = 1853, ComposerID = 1, Description =
            "Superimposed over an only very slightly changed version of the Prelude No. 1 in C major, BWV 846, from Book I of J. S. Bach's The Well-Tempered Clavier."
        }
            });
        }
    }
}
