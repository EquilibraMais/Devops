using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using HealthChecks.SqlServer;

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

        var connectionString = config.GetConnectionString("OracleDb");

        var optionsBuilder = new DbContextOptionsBuilder<EquilibraMaisDbContext>();
        optionsBuilder.UseOracle(connectionString);

        return new EquilibraMaisDbContext(optionsBuilder.Options);
    }
}
