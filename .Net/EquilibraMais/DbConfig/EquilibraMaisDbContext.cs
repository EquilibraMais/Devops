using EquilibraMais.Model;
using Microsoft.EntityFrameworkCore;

namespace EquilibraMais.DbConfig;

public class EquilibraMaisDbContext :DbContext
{
    public EquilibraMaisDbContext(DbContextOptions<EquilibraMaisDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Mapeamento correto das colunas da tabela Usuario e relacionamentos
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Setor_id).HasColumnName("SETOR_ID");
            entity.HasOne(e => e.Setor)
                .WithMany()
                .HasForeignKey(e => e.Setor_id);
        });
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasOne(u => u.Setor)          
                .WithMany()                     
                .HasForeignKey(u => u.Setor_id) 
                .HasConstraintName("FK_USUARIO_SETOR"); 
        });
        
        // Mapeamento correto das colunas da tabela Setor e relacionamentos
        modelBuilder.Entity<Setor>(entity =>
        {
            entity.Property(e => e.Empresa_id).HasColumnName("EMPRESA_ID");
            entity.HasOne(e => e.Empresa)
                .WithMany()
                .HasForeignKey(e => e.Empresa_id);
        });
        modelBuilder.Entity<Setor>(entity =>
        {
            entity.HasOne(s => s.Empresa)          
                .WithMany()                     
                .HasForeignKey(s => s.Empresa_id) 
                .HasConstraintName("FK_SETOR_EMPRESA"); 
        });
        
        modelBuilder.Entity<Funcionario_Info>()
            .HasOne(f => f.Usuario)
            .WithMany() 
            .HasForeignKey(f => f.Usuario_id)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Setor> Setores { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Funcionario_Info> FuncionarioInfos { get; set; }
}