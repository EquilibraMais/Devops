using EquilibraMais.Controller;
using EquilibraMais.DbConfig;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using EquilibraMais.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Conexão para Azure SQL Database
        builder.Services.AddDbContext<EquilibraMaisDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDb"))); // IMPORTANTE: Nome da connection string

        builder.Services.AddOpenApi();
        
        // Grupo de HealthChecks para SQL Server
        builder.Services.AddHealthChecks()
            .AddSqlServer(
                builder.Configuration.GetConnectionString("AzureSqlDb"),
                healthQuery: "SELECT 1",
                name: "sqlserver",
                tags: new[] { "db", "sql" }
            )
            .AddDbContextCheck<EquilibraMaisDbContext>(
                name: "efcore",
                tags: new[] { "dbcontext" }
            );

        builder.Services.AddScoped<RelatorioService>();
        
        var app = builder.Build();
        
        // Verifica o grupo de HealthCheck e retorna um Json com os status
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(e => new {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description
                    })
                });
                await context.Response.WriteAsync(result);
            }
        });

        // Garante Scalar e OpenAPI SEM limitar ao ambiente de desenvolvimento
        app.MapOpenApi();
        app.MapScalarApiReference();

        var v1 = app.MapGroup("/api/v1");
        var v2 = app.MapGroup("/api/v2");
        
        EmpresaEndpoints.Map(v1);
        Funcionario_InfoEndpoints.Map(v1);
        SetorEndpoints.Map(v1);
        UsuarioEndpoints.Map(v1);
        RelatorioEndpoints.Map(v2);

        await app.RunAsync();
    }
}
