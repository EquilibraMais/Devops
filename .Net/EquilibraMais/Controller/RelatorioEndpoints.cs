using EquilibraMais.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquilibraMais.Controller;

public class RelatorioEndpoints
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGroup("/relatorios").WithTags("Relatorios");
        
        group.MapGet("relatorios/humor", async ([FromServices] RelatorioService service) =>
            {
                var resultado = await service.GerarRelatorioHumorAsync();
                return Results.Ok(resultado);
            })
            .WithSummary("Gera relatório de humor dos funcionários")
            .WithDescription("Retorna um relatório agregando as médias de humor, energia, carga e sono por setor e empresa.");

        
        group.MapGet("/humor-medio-por-setor", async ([FromServices] RelatorioService relatorioService) =>
            {
                var dados = await relatorioService.ObterHumorMedioPorSetorAsync();
                return Results.Ok(dados);
            })
            .Produces(StatusCodes.Status200OK)
            .WithSummary("Relatório de humor médio por setor")
            .WithDescription("Retorna o humor médio dos funcionários agrupados por setor.");
    }
}