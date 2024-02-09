using EvolveDb;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Tribe_OAuth2_BE_Demo.config.Database
{
    public static class EvolveConfigurer
    {
        public static void Configure(IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
                var migrationLocation = configuration.GetValue<string>("DatabaseSettings:MigrationLocation");

                var cnx = new SqlConnection(connectionString);
                var evolve = new Evolve(cnx, msg => Console.WriteLine(msg))
                {
                    Locations = new[] { migrationLocation },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
