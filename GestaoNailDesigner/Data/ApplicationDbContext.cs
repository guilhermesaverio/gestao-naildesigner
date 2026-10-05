using GestaoNailDesigner.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoNailDesigner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Aluna> Alunas { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pacote> Pacotes { get; set; }
        public DbSet<PacoteCliente> PacotesClientes { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<TransacaoFinanceira> TransacoesFinanceiras { get; set; }
        public DbSet<InscricaoCurso> InscricoesCursos { get; set; }
    }
}