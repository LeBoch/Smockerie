// Data/BoutiqueContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BoutiqueApi.Data
{
    public class BoutiqueContextFactory : IDesignTimeDbContextFactory<BoutiqueContext>
    {
        public BoutiqueContext CreateDbContext(string[] args)
        {
            // On lit l'appsettings.json PUR
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<BoutiqueContext>();
            builder.UseMySql(
                config.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 33))
            );

            return new BoutiqueContext(builder.Options);
        }
    }
}
