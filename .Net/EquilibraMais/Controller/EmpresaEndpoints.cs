using EquilibraMais.DbConfig;
using EquilibraMais.Model;
using Microsoft.EntityFrameworkCore;

namespace EquilibraMais.Controller;

public class EmpresaEndpoints
{
    public static void Map(RouteGroupBuilder group)
    {
    group.MapGroup("/empresas").WithTags("Empresa");
        
        //Get all
        group.MapGet("/empresas", async (EquilibraMaisDbContext db) =>
            await db.Empresas.ToListAsync())
            .Produces<Empresa>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Retorna todas as empresas")
            .WithDescription("Retorna todas as empresas cadastradas no banco de dados, " +
                             "mesmo que só seja encontrado uma empresa, ele ainda vai retornar uma lista");

        //GetById
        group.MapGet("/empresas/{id}", async (int id, EquilibraMaisDbContext db) =>
        {
            var empresa = await db.Empresas.FindAsync(id);
            return empresa is not null ? Results.Ok(empresa) : Results.NotFound();
        })
        .Produces<Empresa>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Busca uma empresa pelo ID")
        .WithDescription("Retorna os dados de uma empresa específica com base no ID informado. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Inserir
        group.MapPost("/empresas/inserir", async (Empresa empresa, EquilibraMaisDbContext db) =>
            {
                if (empresa == null)
                    return Results.BadRequest("Dados inválidos.");
                
                db.Empresas.Add(empresa);
                await db.SaveChangesAsync();
                return Results.Created($"/Empresas/{empresa.Id}", empresa);
            })
            .Produces<Empresa>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Accepts<Empresa>("application/json")
            .WithSummary("Insere uma nova empresa")
            .WithDescription("Adiciona uma nova empresa ao banco de dados com base nos dados enviados no corpo da requisição.");
        
        // Atualizar
        group.MapPut("/empresas/atualizar/{id}", async (int id, Empresa empresa, EquilibraMaisDbContext db) =>
        {
            var existing = await db.Empresas.FindAsync(id);
            if (existing == null) 
                return Results.NotFound();

            existing.Nome_empresa = empresa.Nome_empresa;
            await db.SaveChangesAsync();

            return Results.Ok($"Empresa com ID {id} atualizada com sucesso.");
        })
        .Produces<Empresa>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<Empresa>("application/json")
        .WithSummary("Atualiza uma empresa existente")
        .WithDescription("Atualiza os dados de uma empresa já cadastrada, identificado pelo ID. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Deletar
        group.MapDelete("/empresas/deletar/{id}", async (int id, EquilibraMaisDbContext db) =>
            {
                var empresa = await db.Empresas.FindAsync(id);
                if (empresa == null) 
                    return Results.NotFound();

                db.Empresas.Remove(empresa);
                await db.SaveChangesAsync();
                
                return Results.Ok($"Empresa com ID {id} removida com sucesso.");
            })
            .Produces<Empresa>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Remove uma empresa")
        .WithDescription("Remove uma empresa do banco de dados com base no ID informado. " +
                         "Caso a empresa não seja encontrada, retorna 404 Not Found.");
    }
}