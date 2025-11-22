using EquilibraMais.DbConfig;
using EquilibraMais.Model;
using Microsoft.EntityFrameworkCore;

namespace EquilibraMais.Controller;

public class SetorEndpoints
{
    public static void Map(RouteGroupBuilder group)
    {
    group.MapGroup("/setores").WithTags("Setor");
        
        //Get all
        group.MapGet("/setores", async (EquilibraMaisDbContext db) =>
            await db.Setores
                .Include(s => s.Empresa)
                .ToListAsync())
            .Produces<Setor>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Retorna todos os setores")
            .WithDescription("Retorna todos os setores cadastrados no banco de dados, " +
                             "mesmo que só seja encontrado um setor, ele ainda vai retornar uma lista");

        //GetById
        group.MapGet("/setores/{id}", async (int id, EquilibraMaisDbContext db) =>
        {
            var setor = await db.Setores
                .Include(s => s.Empresa)
                .FirstOrDefaultAsync(s => s.Id == id);
            return setor is not null ? Results.Ok(setor) : Results.NotFound();
        })
        .Produces<Setor>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Busca um setor pelo ID")
        .WithDescription("Retorna os dados de um setor específico com base no ID informado. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Inserir
        group.MapPost("/setores/inserir", async (Setor setor, EquilibraMaisDbContext db) =>
            {
                if (setor == null)
                    return Results.BadRequest("Dados inválidos.");
                
                db.Setores.Add(setor);
                await db.SaveChangesAsync();
                return Results.Created($"/setores/{setor.Id}", setor);
            })
            .Produces<Setor>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Accepts<Setor>("application/json")
            .WithSummary("Insere um novo setor")
            .WithDescription("Adiciona um novo setor ao banco de dados com base nos dados enviados no corpo da requisição.");
        
        // Atualizar
        group.MapPut("/setores/atualizar/{id}", async (int id, Setor setor, EquilibraMaisDbContext db) =>
        {
            var existing = await db.Setores.FindAsync(id);
            if (existing == null) 
                return Results.NotFound();

            existing.Descricao = setor.Descricao;
            await db.SaveChangesAsync();

            return Results.Ok($"Setor com ID {id} atualizado com sucesso.");
        })
        .Produces<Setor>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<Setor>("application/json")
        .WithSummary("Atualiza um setor existente")
        .WithDescription("Atualiza os dados de um setor já cadastrada, identificado pelo ID. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Deletar
        group.MapDelete("/setores/deletar/{id}", async (int id, EquilibraMaisDbContext db) =>
            {
                var setor = await db.Setores.FindAsync(id);
                if (setor == null) 
                    return Results.NotFound();

                db.Setores.Remove(setor);
                await db.SaveChangesAsync();
                
                return Results.Ok($"Setor com ID {id} removido com sucesso.");
            })
            .Produces<Setor>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Remove um setor")
        .WithDescription("Remove um setor do banco de dados com base no ID informado. " +
                         "Caso o setor não seja encontrada, retorna 404 Not Found.");
    }
}