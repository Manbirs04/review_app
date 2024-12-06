using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ReviewHubBackend.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReviewHubDbContext>
    {
        public ReviewHubDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReviewHubDbContext>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 25))); // Adjust version as needed

            return new ReviewHubDbContext(optionsBuilder.Options);
        }
    }
}
