using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Transavia.Infrastructure;
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

            using (var feed = new HttpFeedDataProvider<AirportData>(Feed, JsonConvert.DeserializeObject<List<AirportData>>))
            using (var dbContext = new TransaviaDbContext(optionsBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
                ClearDatabase(dbContext);

                var seeder = new AirportsSeeder(dbContext, feed);
                seeder.SeedAsync(x => x.continent == "EU", CancellationToken.None).Wait();
            }

            Console.WriteLine("Done");
        }
        
        private static void ClearDatabase(TransaviaDbContext dbContext)
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
