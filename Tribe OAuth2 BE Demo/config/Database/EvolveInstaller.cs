using EvolveDb;
using System;
using System.Data.SqlClient;

namespace Tribe_OAuth2_BE_Demo.config.Database
{
    public static class EvolveInstaller
    {
        public static void Configure(string connectionString, string migrationLocation)
        {
            try
            {
                var cnx = new SqlConnection(connectionString);
                //var cnx = new SqliteConnection(Configuration.GetConnectionString("MyDatabase"));
                var evolve = new Evolve(cnx)
                {
                    Locations = new[] { migrationLocation },
                    IsEraseDisabled = true,
                };
                //.Migrate(cnx, msg => Console.WriteLine(msg))
                //{
                //    Locations = new[] { migrationLocation },
                //    IsEraseDisabled = true,
                //};

                evolve.Migrate();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
