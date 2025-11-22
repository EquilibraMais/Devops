using EquilibraMais.DbConfig;
using Microsoft.EntityFrameworkCore;
using EquilibraMais.Model;

namespace EquilibraMais.Services;

public class RelatorioService
{
    private readonly EquilibraMaisDbContext _db;

    public RelatorioService(EquilibraMaisDbContext db)
    {
        _db = db;
    }

    public async Task<List<object>> GerarRelatorioHumorAsync()
    {
        var relatorio = await _db.FuncionarioInfos
            .Include(f => f.Usuario)
            .ThenInclude(u => u.Setor)
            .ThenInclude(s => s.Empresa)
            .GroupBy(f => new {
                Empresa = f.Usuario.Setor.Empresa.Nome_empresa, 
                Setor = f.Usuario.Setor.Descricao
            })
            .Select(g => new
            {
                NomeEmpresa = g.Key.Empresa,
                NomeSetor = g.Key.Setor,
                TotalRegistros = g.Count(),
                MediaHumor = g.Average(f => f.Humor),
                MediaEnergia = g.Average(f => f.Energia),
                MediaCarga = g.Average(f => f.Carga),
                MediaSono = g.Average(f => f.Sono)
            })
            .ToListAsync();

        return relatorio.Cast<object>().ToList();
    }
    
    public async Task<List<object>> ObterHumorMedioPorSetorAsync()
    {
        var query = await _db.FuncionarioInfos
            .Include(f => f.Usuario)
            .ThenInclude(u => u.Setor)
            .GroupBy(f => f.Usuario.Setor.Descricao)
            .Select(g => new 
            { 
                Setor = g.Key, 
                HumorMedio = g.Average(x => x.Humor)
            })
            .ToListAsync();

        return query.Cast<object>().ToList();
    }
}