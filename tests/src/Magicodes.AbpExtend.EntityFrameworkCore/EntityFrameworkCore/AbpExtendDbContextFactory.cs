using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Magicodes.AbpExtend.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class AbpExtendDbContextFactory : IDesignTimeDbContextFactory<AbpExtendDbContext>
    {
        public AbpExtendDbContext CreateDbContext(string[] args)
        {
            AbpExtendEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AbpExtendDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new AbpExtendDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Magicodes.AbpExtend.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
