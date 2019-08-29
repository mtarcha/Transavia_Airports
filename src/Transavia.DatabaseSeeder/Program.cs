using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Transavia.Infrastructure.Data;
using Utility.CommandLine;

namespace Transavia.DatabaseSeeder
{
    class Program
    {
        [Argument('f', "feed", "http(s) feed to download airports data")]
        private static string Feed { get; set; }

        [Argument('c', "ConnectionString", "DB connection string")]
        private static string ConnectionString { get; set; }

        static void Main(string[] args)
        {
            Arguments.Populate();

            var optionsBuilder = new DbContextOptionsBuilder<TransaviaDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            using (var feed = new HttpFeedDataProvider(Feed))
            using (var dbContext = new TransaviaDbContext(optionsBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
                var seeder = new AirportsSeeder(feed, dbContext);
                seeder.SeedAsync(x => x.Country.Continent.Code == "EU", CancellationToken.None).Wait();
            }
        }
    }
}
