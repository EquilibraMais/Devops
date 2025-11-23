using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EquilibraMais.DbConfig;

public class EquilibraMaisDbContextFactory : IDesignTimeDbContextFactory<EquilibraMaisDbContext>
{
    public EquilibraMaisDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("AzureSqlDb");

        var optionsBuilder = new DbContextOptionsBuilder<EquilibraMaisDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new EquilibraMaisDbContext(optionsBuilder.Options);
    }
}
