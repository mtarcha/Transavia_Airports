using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Transavia.Infrastructure.Data;
using Utility.CommandLine;

namespace Transavia.DatabaseSeeder
{
    class Program
    {
        // todo: add logging

        [Argument('f', "feed", "http(s) feed to download airports data")]
        private static string Feed { get; set; }

        [Argument('c', "ConnectionString", "DB connection string")]
        private static string ConnectionString { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting database seeding");

            Arguments.Populate();

            var optionsBuilder = new DbContextOptionsBuilder<TransaviaDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            using (var feed = new HttpFeedDataProvider(Feed))
            using (var dbContext = new TransaviaDbContext(optionsBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
                ClearDatabae(dbContext);

                var seeder = new AirportsSeeder(feed, dbContext);
                seeder.SeedAsync(x => x.Country.Continent.Code == "EU", CancellationToken.None).Wait();
            }

            Console.WriteLine("Done");
        }
        
        private static void ClearDatabae(TransaviaDbContext dbContext)
        {
            dbContext.Database.ExecuteSqlCommand(@"
                    -- disable all constraints
                    EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT all'

                    -- delete data in all tables
                    EXEC sp_MSForEachTable 'DELETE FROM ?'

                    -- enable all constraints
                    EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'");
        }
    }
}
