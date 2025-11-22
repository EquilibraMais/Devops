using EquilibraMais.Model;
using EquilibraMais.Services;

namespace EquilibraMais.Tests.ServiceTests;

public class RelatorioServiceTests
{
    [Fact]
    public async Task GerarRelatorioHumorAsync_DeveRetornarCorreto()
    {
        // Arrange
        var db = MockDbFactory.Create();

        var empresa = new Empresa { Id = 1, Nome_empresa = "Empresa A" };
        var setor = new Setor { Id = 1, Descricao = "Recursos Humanos", Empresa = empresa, Empresa_id = empresa.Id };
        var usuario = new Usuario { Id = 1, Nome = "João Silva", Setor = setor, Setor_id = setor.Id, Cargo = "Analista" };

        db.Empresas.Add(empresa);
        db.Setores.Add(setor);
        db.Usuarios.Add(usuario);
        db.FuncionarioInfos.Add(new Funcionario_Info
        {
            Id = 1,
            Usuario = usuario,
            Usuario_id = usuario.Id,
            Humor = 8,
            Energia = 7,
            Carga = 5,
            Sono = 6
        });
        db.SaveChanges();

        var service = new RelatorioService(db);

        // Act
        var resultado = await service.GerarRelatorioHumorAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Single(resultado);
    }
}

