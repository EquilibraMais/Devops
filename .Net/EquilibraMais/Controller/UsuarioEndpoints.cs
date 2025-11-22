using EquilibraMais.DbConfig;
using EquilibraMais.Model;
using Microsoft.EntityFrameworkCore;

namespace EquilibraMais.Controller;

public class UsuarioEndpoints
{
    public static void Map(RouteGroupBuilder group)
    {
    group.MapGroup("/usuarios").WithTags("Usuario");
        
        //Get all
        group.MapGet("/usuarios", async (EquilibraMaisDbContext db) =>
            await db.Usuarios
                .Include(u => u.Setor)
                .ThenInclude(s => s.Empresa)
                .ToListAsync())
            .Produces<Usuario>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Retorna todos os usuários")
            .WithDescription("Retorna todos os usuários cadastrados no banco de dados, " +
                             "mesmo que só seja encontrado um usuário, ele ainda vai retornar uma lista");

        //GetById
        group.MapGet("/usuarios/{id}", async (int id, EquilibraMaisDbContext db) =>
        {
            var usuario = await db.Usuarios
                .Include(u => u.Setor)
                .ThenInclude(s => s.Empresa)  
                .FirstOrDefaultAsync(u => u.Id == id);
            return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
        })
        .Produces<Usuario>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Busca um usuário pelo ID")
        .WithDescription("Retorna os dados de um usuário específico com base no ID informado. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Inserir
        group.MapPost("/usuarios/inserir", async (Usuario usuario, EquilibraMaisDbContext db) =>
            {
                if (usuario == null)
                    return Results.BadRequest("Dados inválidos.");
                
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return Results.Created($"/usuarios/{usuario.Id}", usuario);
            })
            .Produces<Usuario>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Accepts<Usuario>("application/json")
            .WithSummary("Insere um novo usuário")
            .WithDescription("Adiciona um novo usuário ao banco de dados com base nos dados enviados no corpo da requisição.");
        
        // Atualizar
        group.MapPut("/usuarios/atualizar/{id}", async (int id, Usuario usuario, EquilibraMaisDbContext db) =>
        {
            var existing = await db.Usuarios.FindAsync(id);
            if (existing == null) 
                return Results.NotFound();

            existing.Nome = usuario.Nome;
            existing.Cargo = usuario.Cargo;
            await db.SaveChangesAsync();

            return Results.Ok($"Usuário com ID {id} atualizado com sucesso.");
        })
        .Produces<Usuario>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<Usuario>("application/json")
        .WithSummary("Atualiza um usuário existente")
        .WithDescription("Atualiza os dados de um usuário já cadastrada, identificado pelo ID. " +
                         "Caso o ID não exista, retorna 404 Not Found.");
        
        // Deletar
        group.MapDelete("/usuarios/deletar/{id}", async (int id, EquilibraMaisDbContext db) =>
            {
                var usuario = await db.Usuarios.FindAsync(id);
                if (usuario == null) 
                    return Results.NotFound();

                db.Usuarios.Remove(usuario);
                await db.SaveChangesAsync();
                
                return Results.Ok($"Usuário com ID {id} removido com sucesso.");
            })
            .Produces<Usuario>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Remove um usuário")
        .WithDescription("Remove um usuário do banco de dados com base no ID informado. " +
                         "Caso o usuário não seja encontrada, retorna 404 Not Found.");
    }
}